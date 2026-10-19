using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class VoiceCommentEvent : AuditEvent
    {
        [JsonPropertyName("data")]
        public object Data { get; set; }
        [JsonPropertyName("formatted_from")]
        public string FormattedFrom { get; set; }
        [JsonPropertyName("formatted_to")]
        public string FormattedTo { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
        [JsonPropertyName("html_body")]
        public string HtmlBody { get; set; }
        [JsonPropertyName("public")]
        public bool Public { get; set; }
        [JsonPropertyName("trusted")]
        public bool Trusted { get; set; }
        [JsonPropertyName("author_id")]
        public long AuthorId { get; set; }
        [JsonPropertyName("transcription_visible")]
        public bool TranscriptionVisible { get; set; }
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; }
    }
}