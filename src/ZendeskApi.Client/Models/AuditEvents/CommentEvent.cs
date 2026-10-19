using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CommentEvent : AuditEvent
    {
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("html_body")]
        public string HtmlBody { get; set; }
        [JsonPropertyName("plain_body")]
        public string PlainBody { get; set; }
        [JsonPropertyName("public")]
        public bool Public { get; set; }
        [JsonPropertyName("author_id")]
        public long AuthorId { get; set; }
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; }
        [JsonPropertyName("via")]
        public Via Via { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}