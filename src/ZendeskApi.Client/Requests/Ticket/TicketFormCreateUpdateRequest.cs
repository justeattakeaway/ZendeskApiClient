using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class TicketFormCreateUpdateRequest
    {
        public TicketFormCreateUpdateRequest(TicketForm ticketForm)
        {
            TicketForm = ticketForm;
        }

        [JsonPropertyName("ticket_form")]
        public TicketForm TicketForm { get; set; }
    }
}