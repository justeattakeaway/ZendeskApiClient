using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    public class GroupCreateRequest
    {
        public GroupCreateRequest(string name)
        {
            Name = name;
        }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; internal set; }
    }
}