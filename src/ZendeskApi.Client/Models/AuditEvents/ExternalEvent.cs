using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ExternalEvent : AuditEvent
    {
        [JsonPropertyName("resource")]
        public string Resource { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}