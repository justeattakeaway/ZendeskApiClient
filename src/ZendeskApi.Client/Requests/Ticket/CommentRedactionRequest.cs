using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/api-reference/ticketing/tickets/ticket_comments/#redact-ticket-comment-in-agent-workspace
    /// </summary>
    public class CommentRedactionRequest
    {
        public CommentRedactionRequest(long ticketId)
        {
            TicketId = ticketId;
        }

        /// <summary>
        /// The ID of the ticket
        /// </summary>
        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }

        /// <summary>
        /// The comment's html_body containing `&lt;redact&gt;` tags or `redact` attributes
        /// </summary>
        [JsonPropertyName("html_body")]
        public string HtmlBody { get; set; }

        /// <summary>
        /// Attachment URLs belonging to the comment to be redacted. See the content_url property of Attachment
        /// </summary>
        [JsonPropertyName("external_attachment_urls")]
        public IEnumerable<string> ExternalAttachmentUrls { get; set; }
    }
}
