using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    public class GroupUpdateRequest
    {
        public GroupUpdateRequest(long id)
        {
            Id = id;
        }
        public long Id { get; set;  }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; internal set; }
    }
}