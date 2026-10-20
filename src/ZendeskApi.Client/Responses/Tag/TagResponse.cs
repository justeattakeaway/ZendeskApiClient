using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class TagResponse : CursorPaginationResponse<Tag>
    { 
        [JsonPropertyName("tags")]
        public IEnumerable<Tag> Tags { get; set; }

        protected override IEnumerable<Tag> Enumerable => Tags;
    }
}