using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
        }

        [JsonPropertyName("error")]
        [JsonInclude]
        public string Error { get; internal set; }

        [JsonPropertyName("description")]
        [JsonInclude]
        public string Description { get; internal set; }

        [JsonPropertyName("details")]
        [JsonInclude]
        public JsonObject Details { get; internal set; }
    }
}
