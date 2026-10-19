using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class HelpCenterCategoryListCursorResponse : CursorPaginationResponse<HelpCenterCategory>
    {
        [JsonPropertyName("categories")]
        public IEnumerable<HelpCenterCategory> Groups { get; set; }

        protected override IEnumerable<HelpCenterCategory> Enumerable => Groups;
    }
}