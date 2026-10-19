using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class CustomFieldConverter : JsonConverter<CustomField>
    {
        public override void Write(Utf8JsonWriter writer, CustomField customField, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", customField.Id);

            if (customField.Value != null)
            {
                writer.WriteString("value", customField.Value);
            }
            else if (customField.Values == null || customField.Values.Count == 0)
            {
                writer.WriteNull("value");
            }
            else
            {
                writer.WriteStartArray("value");
                foreach (var value in customField.Values)
                {
                    writer.WriteStringValue(value);
                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }

        public override CustomField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                var customField = new CustomField
                {
                    Id = root.GetProperty("id").GetInt64()
                };

                if (!root.TryGetProperty("value", out var valueElement))
                {
                    return customField;
                }

                if (valueElement.ValueKind == JsonValueKind.Array)
                {
                    var values = new List<string>();
                    foreach (var item in valueElement.EnumerateArray())
                    {
                        values.Add(ToStringValue(item));
                    }
                    customField.Values = values;
                }
                else
                {
                    customField.Value = ToStringValue(valueElement);
                }

                return customField;
            }
        }

        private static string ToStringValue(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Null:
                case JsonValueKind.Undefined:
                    return null;
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.True:
                    return bool.TrueString;
                case JsonValueKind.False:
                    return bool.FalseString;
                default:
                    return element.GetRawText();
            }
        }
    }
}
