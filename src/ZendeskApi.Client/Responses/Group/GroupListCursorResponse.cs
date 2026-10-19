using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class GroupListCursorResponse : CursorPaginationResponse<Group>
    {
        [JsonPropertyName("groups")]
        public IEnumerable<Group> Groups { get; set; }

        protected override IEnumerable<Group> Enumerable => Groups;
    }
}