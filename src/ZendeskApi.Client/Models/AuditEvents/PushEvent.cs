using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class PushEvent : AuditEvent
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("value_reference")]
        public string ValueReference { get; set; }
    }
}