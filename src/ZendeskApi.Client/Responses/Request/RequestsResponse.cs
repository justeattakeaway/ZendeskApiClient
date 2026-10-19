using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class RequestsResponse : PaginationResponse<Request>
    {
        [JsonPropertyName("requests")]
        public IEnumerable<Request> Requests { get; set; }

        protected override IEnumerable<Request> Enumerable => Requests;
    }
}
