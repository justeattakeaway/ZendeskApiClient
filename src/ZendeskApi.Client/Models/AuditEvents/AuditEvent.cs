using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class AuditEvent : IAuditEvent
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("type")]
        public AuditTypes Type { get; set; }
    }
}