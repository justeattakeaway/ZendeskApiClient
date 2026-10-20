using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleHelpCenterArticleResponse
    {
        [JsonPropertyName("article")]
        public HelpCenterArticle Article { get; set; }
    }
}