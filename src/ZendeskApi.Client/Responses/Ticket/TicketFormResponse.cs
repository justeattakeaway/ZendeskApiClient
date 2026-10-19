using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFormResponse
    {
        [JsonPropertyName("ticket_form")]
        public TicketForm TicketForm { get; set; }
    }
}