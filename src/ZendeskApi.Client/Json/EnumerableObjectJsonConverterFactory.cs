using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// The paginated response types implement <see cref="System.Collections.IEnumerable"/> for convenience, but are
    /// JSON objects. System.Text.Json always treats enumerable types as JSON arrays, so these types are read and
    /// written property by property instead.
    /// </summary>
    internal class EnumerableObjectJsonConverterFactory : JsonConverterFactory
    {
        private static readonly Type[] BaseTypes =
        {
            typeof(PaginationResponse<>),
            typeof(CursorPaginationResponse<>),
            typeof(CursorPaginationVariantResponse<>)
        };

        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsClass || typeToConvert.IsAbstract)
            {
                return false;
            }

            for (var type = typeToConvert.BaseType; type != null; type = type.BaseType)
            {
                if (type.IsGenericType && BaseTypes.Contains(type.GetGenericTypeDefinition()))
                {
                    return true;
                }
            }

            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(Converter<>).MakeGenericType(typeToConvert));
        }

        private class Converter<T> : JsonConverter<T>
            where T : class
        {
            private static readonly List<Member> Members = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(p => p.GetIndexParameters().Length == 0 && p.GetCustomAttribute<JsonIgnoreAttribute>() == null)
                .Select(p => new Member(p))
                .Where(m => m.IsSerialized)
                .ToList();

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException($"Expected a JSON object when reading {typeof(T).Name}.");
                }

                var result = (T)Activator.CreateInstance(typeof(T), true);
                var comparison = options.PropertyNameCaseInsensitive
                    ? StringComparison.OrdinalIgnoreCase
                    : StringComparison.Ordinal;

                using (var document = JsonDocument.ParseValue(ref reader))
                {
                    foreach (var property in document.RootElement.EnumerateObject())
                    {
                        var member = Members.FirstOrDefault(m => m.CanRead && string.Equals(m.Name, property.Name, comparison));

                        if (member == null)
                        {
                            continue;
                        }

                        var value = property.Value.Deserialize(member.Property.PropertyType, options);

                        if (value == null && member.Property.PropertyType.IsValueType && Nullable.GetUnderlyingType(member.Property.PropertyType) == null)
                        {
                            continue;
                        }

                        member.Property.SetValue(result, value);
                    }
                }

                return result;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                foreach (var member in Members.Where(m => m.CanWrite))
                {
                    var memberValue = member.Property.GetValue(value);

                    if (memberValue == null && options.DefaultIgnoreCondition == JsonIgnoreCondition.WhenWritingNull)
                    {
                        continue;
                    }

                    writer.WritePropertyName(member.Name);
                    JsonSerializer.Serialize(writer, memberValue, member.Property.PropertyType, options);
                }

                writer.WriteEndObject();
            }
        }

        private class Member
        {
            public Member(PropertyInfo property)
            {
                Property = property;

                var name = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                Name = name?.Name ?? property.Name;

                var hasAttribute = name != null || property.GetCustomAttribute<JsonIncludeAttribute>() != null;
                var getter = property.GetGetMethod(true);
                var setter = property.GetSetMethod(true);

                CanWrite = getter != null && (getter.IsPublic || hasAttribute);
                CanRead = setter != null && (setter.IsPublic || hasAttribute);
            }

            public PropertyInfo Property { get; }

            public string Name { get; }

            public bool CanRead { get; }

            public bool CanWrite { get; }

            public bool IsSerialized => CanRead || CanWrite;
        }
    }
}
