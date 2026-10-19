using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterSectionResponse
    {
        [JsonPropertyName("section")]
        public HelpCenterSection Section { get; set; }
    }
}