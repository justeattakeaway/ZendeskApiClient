using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    internal class RequestCreateRequest
    {
        public RequestCreateRequest(Request request)
        {
            Request = request;
        }

        [JsonPropertyName("request")]
        public Request Request { get; set; }
    }
}