using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketResponse
    {
        [JsonPropertyName("ticket")]
        public Ticket Ticket { get; set; }
    }
}