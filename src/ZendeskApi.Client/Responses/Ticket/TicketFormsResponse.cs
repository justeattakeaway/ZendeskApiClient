using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketFormsResponse : PaginationResponse<TicketForm>
    {
        [JsonPropertyName("ticket_forms")]
        public IEnumerable<TicketForm> TicketForms { get; set; }

        protected override IEnumerable<TicketForm> Enumerable => TicketForms;
    }
}
