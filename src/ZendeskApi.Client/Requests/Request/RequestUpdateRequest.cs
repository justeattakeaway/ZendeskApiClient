using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    internal class RequestUpdateRequest
    {
        public RequestUpdateRequest(Request request)
        {
            Request = request;
        }

        [JsonPropertyName("request")]
        public Request Request { get; set; }
    }
}