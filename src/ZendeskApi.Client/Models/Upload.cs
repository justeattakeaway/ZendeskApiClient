using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class Upload
    {
        [JsonIgnore]
        public long? Id { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("attachment")]
        public Attachment Attachment { get; set; }
    }
}
