using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class GroupListRequest<T>
    {
        [JsonPropertyName("groups")]
        public IEnumerable<T> Groups { get; set; }

        public GroupListRequest(IEnumerable<T> groups)
        {
            Groups = groups;
        }
    }
}