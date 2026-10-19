using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public class SearchResponse<T> : PaginationResponse<T> where T : ISearchResult
    {
        [JsonPropertyName("results")]
        public IEnumerable<T> Results { get; set; }
        
        protected override IEnumerable<T> Enumerable => Results;
    }
}