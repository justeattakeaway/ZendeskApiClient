using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class AttachmentResponse
    {
        [JsonPropertyName("attachment")]
        public Attachment Attachment { get; set; }
    }
}
