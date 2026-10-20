using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class TicketComment
    {
        public long? Id { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TicketEventType Type { get; set; }

        [JsonPropertyName("html_body")]
        public string HtmlBody { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("author_id")]
        public long? Author { get; set; }

        [JsonPropertyName("public")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("attachments")]
        [JsonInclude]
        public List<Attachment> Attachments { get; private set; }

        [JsonPropertyName("uploads")]
        public List<string> Uploads { get; set; }


        [JsonPropertyName("via")]
        public Via Via { get; set; }
        public void AddAttachmentToComment(string attachmentToken)
        {
            Uploads = Uploads ?? new List<string>();

            Uploads.Add(attachmentToken);
        }

    }
}