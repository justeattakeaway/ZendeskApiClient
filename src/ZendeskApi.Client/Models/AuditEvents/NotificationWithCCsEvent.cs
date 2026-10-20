using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class NotificationWithCCsEvent : AuditEvent
    {
        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("recipients")]
        public long[] Recipients { get; set; }
        [JsonPropertyName("via")]
        public Via Via { get; set; }
    }
}