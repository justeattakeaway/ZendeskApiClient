using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// System.Text.Json does not support <see cref="object"/> as a dictionary key; keys are always strings in JSON.
    /// </summary>
    internal class ObjectDictionaryJsonConverter : JsonConverter<Dictionary<object, object>>
    {
        public override Dictionary<object, object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var values = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);

            if (values == null)
            {
                return null;
            }

            var result = new Dictionary<object, object>(values.Count);
            foreach (var pair in values)
            {
                result[pair.Key] = pair.Value;
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<object, object> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var pair in value)
            {
                writer.WritePropertyName(Convert.ToString(pair.Key, CultureInfo.InvariantCulture) ?? string.Empty);

                if (pair.Value == null)
                {
                    writer.WriteNullValue();
                }
                else
                {
                    JsonSerializer.Serialize(writer, pair.Value, pair.Value.GetType(), options);
                }
            }

            writer.WriteEndObject();
        }
    }
}
