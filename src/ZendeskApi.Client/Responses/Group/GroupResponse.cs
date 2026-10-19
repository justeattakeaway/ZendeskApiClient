using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class GroupResponse
    {
        [JsonPropertyName("group")]
        public Group Group { get; set; }
    }
}