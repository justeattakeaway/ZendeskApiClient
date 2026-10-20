using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TicketCommentsResponse : PaginationResponse<TicketComment>
    {
        [JsonPropertyName("comments")]
        public IEnumerable<TicketComment> Comments { get; set; }
        
        protected override IEnumerable<TicketComment> Enumerable => Comments;
    }
}
