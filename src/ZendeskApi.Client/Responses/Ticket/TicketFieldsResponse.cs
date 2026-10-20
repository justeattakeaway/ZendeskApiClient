using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFieldsResponse : PaginationResponse<TicketField>
    {
        [JsonPropertyName("ticket_fields")]
        [JsonInclude]
        public IEnumerable<TicketField> TicketFields { get; internal set; }

        protected override IEnumerable<TicketField> Enumerable => TicketFields;
    }
}
