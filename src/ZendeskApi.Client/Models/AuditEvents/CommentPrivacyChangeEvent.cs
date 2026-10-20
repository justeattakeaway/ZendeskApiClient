using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentPrivacyChangeEvent : AuditEvent
    {
        [JsonPropertyName("comment_id")]
        public long CommentId { get; set; }
        [JsonPropertyName("public")]
        public bool Public { get; set; }
    }
}