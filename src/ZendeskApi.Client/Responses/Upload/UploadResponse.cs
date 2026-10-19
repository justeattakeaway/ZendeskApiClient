using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UploadResponse
    {
        [JsonPropertyName("upload")]
        public Upload Upload { get; set; }
    }
}
