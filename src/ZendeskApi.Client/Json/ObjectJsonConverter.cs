using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// Reads <see cref="object"/> typed values as CLR primitives (string, long, double, bool) where possible,
    /// and as a <see cref="JsonElement"/> for objects and arrays.
    /// </summary>
    internal class ObjectJsonConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();
                case JsonTokenType.Number:
                    return reader.TryGetInt64(out var l) ? l : reader.GetDouble();
                case JsonTokenType.True:
                    return true;
                case JsonTokenType.False:
                    return false;
                case JsonTokenType.Null:
                    return null;
                default:
                    using (var document = JsonDocument.ParseValue(ref reader))
                    {
                        return document.RootElement.Clone();
                    }
            }
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            var type = value.GetType();

            if (type == typeof(object))
            {
                writer.WriteStartObject();
                writer.WriteEndObject();
                return;
            }

            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
}
