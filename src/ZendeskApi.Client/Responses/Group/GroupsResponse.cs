using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Responses
{
    public class GroupListResponse : PaginationResponse<Group>
    {
        [JsonPropertyName("groups")]
        public IEnumerable<Group> Groups { get; set; }
        
        protected override IEnumerable<Group> Enumerable => Groups;
    }
}
