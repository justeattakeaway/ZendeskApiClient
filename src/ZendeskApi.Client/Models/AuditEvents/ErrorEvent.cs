using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ErrorEvent : AuditEvent
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}