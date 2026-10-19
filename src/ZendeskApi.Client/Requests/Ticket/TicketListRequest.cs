using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class TicketListRequest<T>
    {
        public TicketListRequest(IEnumerable<T> tickets)
        {
            Tickets = tickets;
        }

        [JsonPropertyName("tickets")]
        public IEnumerable<T> Tickets { get; set; }
    }
}