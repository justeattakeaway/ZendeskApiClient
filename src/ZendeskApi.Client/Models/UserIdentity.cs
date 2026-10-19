using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class UserIdentity
    {
        public long? Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("primary")]
        public bool Primary { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
