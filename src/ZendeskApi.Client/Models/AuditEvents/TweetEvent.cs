using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class TweetEvent : AuditEvent
    {
        [JsonPropertyName("direct_message")]
        public bool DirectMessage { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("recipients")]
        public long[] Recipients { get; set; }
    }
}