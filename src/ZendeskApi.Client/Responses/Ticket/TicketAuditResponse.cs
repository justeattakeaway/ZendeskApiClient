using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketAuditResponse : CursorPaginationVariantResponse<TicketAudit>
    {
        [JsonPropertyName("audits")]
        public IEnumerable<TicketAudit> Audits { get; set; }

        protected override IEnumerable<TicketAudit> Enumerable => Audits;
    }
}