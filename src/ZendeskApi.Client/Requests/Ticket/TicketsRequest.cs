using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Requests
{
    public class TicketsRequest
    {
        [JsonPropertyName("tickets")]
        public IEnumerable<Ticket> Item { get; set; }
    }
}
