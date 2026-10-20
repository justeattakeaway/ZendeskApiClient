using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class SatisfactionRatingEvent : AuditEvent
    {
        [JsonPropertyName("score")]
        public string Score { get; set; }
        [JsonPropertyName("assignee_id")]
        public long AssigneeId { get; set; }
        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}