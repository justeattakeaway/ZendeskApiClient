using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class AttachmentRedactionEvent : AuditEvent
    {
        [JsonPropertyName("attachment_id")]
        public long AttachmentId { get; set; }
        [JsonPropertyName("comment_id")]
        public long CommentId { get; set; }
    }
}