using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FacebookEvent : AuditEvent
    {
        [JsonPropertyName("communication")]
        public int Communication { get; set; }
        [JsonPropertyName("ticket_via")]
        public string TicketVia { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        
    }
}