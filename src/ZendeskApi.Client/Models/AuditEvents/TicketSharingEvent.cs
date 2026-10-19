using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class TicketSharingEvent : AuditEvent
    {
        [JsonPropertyName("agreement_id")]
        public long AgreementId { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
    }
}