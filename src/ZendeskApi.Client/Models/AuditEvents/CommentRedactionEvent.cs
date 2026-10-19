using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentRedactionEvent : AuditEvent
    {
        [JsonPropertyName("comment_id")]
        public long CommentId { get; set; }
    }
}