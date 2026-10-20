using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class HelpCenterArticleListResponse : PaginationResponse<HelpCenterArticle>
    {
        [JsonPropertyName("articles")]
        public IEnumerable<HelpCenterArticle> Articles { get; set; }

        protected override IEnumerable<HelpCenterArticle> Enumerable => Articles;
    }
}