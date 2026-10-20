using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationResponse
    {
        [JsonPropertyName("organization")]
        public Organization Organization { get; set; }
    }
}
