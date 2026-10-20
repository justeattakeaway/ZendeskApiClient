using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CCEvent : AuditEvent
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("recipients")]
        public long[] Recipients { get; set; }
        [JsonPropertyName("via")]
        public Via Via { get; set; }
    }
}