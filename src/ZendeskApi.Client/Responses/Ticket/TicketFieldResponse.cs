using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFieldResponse
    {
        [JsonPropertyName("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}