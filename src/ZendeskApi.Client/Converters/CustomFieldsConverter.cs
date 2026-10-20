using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class CustomFieldsConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert == typeof(IReadOnlyCustomFields) ||
                   typeToConvert == typeof(ICustomFields);
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return typeToConvert == typeof(IReadOnlyCustomFields)
                ? (JsonConverter)new Converter<IReadOnlyCustomFields>()
                : new Converter<ICustomFields>();
        }

        private class Converter<T> : JsonConverter<T>
            where T : class
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return JsonSerializer.Deserialize<CustomFields>(ref reader, options) as T;
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
            }
        }
    }
}
