using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FacebookCommentEvent : AuditEvent
    {
        [JsonPropertyName("data")]
        public object Data { get; set; }
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
        [JsonPropertyName("graph_object_id")]
        public string GraphObjectId { get; set; }
    }
}