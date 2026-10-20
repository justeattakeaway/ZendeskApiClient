using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class RequestResponse
    {
        [JsonPropertyName("request")]
        public Request Request { get; set; }
    }
}