using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class HelpCenterSectionListCursorResponse : CursorPaginationResponse<HelpCenterSection>
    {
        [JsonPropertyName("sections")]
        public IEnumerable<HelpCenterSection> Sections { get; set; }

        protected override IEnumerable<HelpCenterSection> Enumerable => Sections;
    }
}