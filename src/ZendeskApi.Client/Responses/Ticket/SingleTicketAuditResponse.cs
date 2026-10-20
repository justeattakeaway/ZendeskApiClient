using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleTicketAuditResponse
    {
        [JsonPropertyName("audit")]
        public TicketAudit Audit { get; set; }
    }
}