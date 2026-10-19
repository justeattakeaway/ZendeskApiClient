using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// System.Text.Json writes values using their declared type, which drops the properties of the implementing
    /// class when the declared type is one of this library's interfaces (for example a list of search results).
    /// Values of those interfaces are written using their runtime type instead.
    /// </summary>
    internal class RuntimeTypeJsonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsInterface &&
                   typeToConvert.Namespace != null &&
                   typeToConvert.Namespace.StartsWith("ZendeskApi.Client", StringComparison.Ordinal) &&
                   !typeToConvert.IsGenericType;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(typeof(Converter<>).MakeGenericType(typeToConvert));
        }

        private class Converter<T> : JsonConverter<T>
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                throw new NotSupportedException($"{typeToConvert.Name} cannot be deserialized, use a concrete type.");
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }
}
