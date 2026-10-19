using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class LogMeInTranscriptEvent : AuditEvent
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}