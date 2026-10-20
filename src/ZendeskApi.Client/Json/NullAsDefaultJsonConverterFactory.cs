using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// Zendesk sometimes returns null for properties that are modelled as non-nullable values.
    /// Those nulls are read as the default value instead of failing.
    /// </summary>
    internal class NullAsDefaultJsonConverterFactory : JsonConverterFactory
    {
        private static readonly Type[] SupportedTypes =
        {
            typeof(bool), typeof(byte), typeof(short), typeof(int), typeof(long),
            typeof(float), typeof(double), typeof(decimal),
            typeof(DateTime), typeof(DateTimeOffset), typeof(Guid), typeof(TimeSpan)
        };

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum || SupportedTypes.Contains(typeToConvert);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            // The inner options are the same as the outer ones, without this factory, so that the built in converters are used.
            var innerOptions = new JsonSerializerOptions(options);
            foreach (var converter in innerOptions.Converters.OfType<NullAsDefaultJsonConverterFactory>().ToList())
            {
                innerOptions.Converters.Remove(converter);
            }

            return (JsonConverter)Activator.CreateInstance(
                typeof(Converter<>).MakeGenericType(typeToConvert),
                innerOptions);
        }

        private class Converter<T> : JsonConverter<T>
            where T : struct
        {
            private readonly JsonSerializerOptions _innerOptions;

            public Converter(JsonSerializerOptions innerOptions)
            {
                _innerOptions = innerOptions;
            }

            public override bool HandleNull => true;

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(ref reader, _innerOptions);
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, _innerOptions);
            }
        }
    }
}
