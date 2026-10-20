using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Converters
{
    public class JobStatusResultConverter : JsonConverter<IEnumerable<JobStatusResult>>
    {
        public override void Write(Utf8JsonWriter writer, IEnumerable<JobStatusResult> value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        public override IEnumerable<JobStatusResult> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                    var itemAsResult = JsonSerializer.Deserialize<JobStatusResult>(ref reader, options);
                    return new[] { itemAsResult };
                case JsonTokenType.StartArray:
                    return JsonSerializer.Deserialize<List<JobStatusResult>>(ref reader, options);
            }

            //We shouldn't ever be getting something other than an Object/Array.
            reader.Skip();
            return null;
        }
    }
}
