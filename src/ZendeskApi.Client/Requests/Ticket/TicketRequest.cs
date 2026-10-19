using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class TicketRequest<T>
    {
        public TicketRequest(T ticket)
        {
            Ticket = ticket;
        }

        [JsonPropertyName("ticket")]
        public T Ticket { get; set; }
    }
}