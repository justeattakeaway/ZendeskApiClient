using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// Reads JSON numbers and booleans into strings instead of failing.
    /// </summary>
    internal class LenientStringJsonConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    using (var document = JsonDocument.ParseValue(ref reader))
                    {
                        return document.RootElement.GetRawText();
                    }
                case JsonTokenType.True:
                    return bool.TrueString;
                case JsonTokenType.False:
                    return bool.FalseString;
                case JsonTokenType.Null:
                    return null;
                default:
                    throw new JsonException($"Cannot convert a JSON {reader.TokenType} to a string.");
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }
    }
}
