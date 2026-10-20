using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterCategoryResponse
    {
        [JsonPropertyName("category")]
        public HelpCenterCategory Category { get; set; }
    }
}