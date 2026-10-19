using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class Via
    {
        [JsonPropertyName("channel")]
        public string Channel { get; set; }

        [JsonPropertyName("source")]
        public Source Source { get; set; }
    }
    
    public class Source
    {
        [JsonPropertyName("to")]
        public dynamic To { get; set; }

        [JsonPropertyName("from")]
        public dynamic From { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }
    }
}
