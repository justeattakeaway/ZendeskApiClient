using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/support/ticket_fields#create-ticket-field
    /// </summary>
    public class TicketFieldCreateUpdateRequest
    {
        public TicketFieldCreateUpdateRequest(TicketField ticketField)
        {
            TicketField = ticketField;
        }
        
        [JsonPropertyName("ticket_field")]
        public TicketField TicketField { get; set; }
    }
}