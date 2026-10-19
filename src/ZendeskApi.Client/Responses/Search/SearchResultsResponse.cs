using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public class SearchResultsResponse : PaginationResponse<ISearchResult>
    {
        [JsonPropertyName("results")]
        public IEnumerable<ISearchResult> Results { get; set; }

        protected override IEnumerable<ISearchResult> Enumerable => Results;
    }
    
}
