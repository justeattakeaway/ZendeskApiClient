using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketsListCursorResponse : CursorPaginationResponse<Ticket>
    {
        [JsonPropertyName("tickets")]
        public IEnumerable<Ticket> Tickets { get; set; }

        protected override IEnumerable<Ticket> Enumerable => Tickets;
    }
}
