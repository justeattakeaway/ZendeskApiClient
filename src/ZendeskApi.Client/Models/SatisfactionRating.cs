using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class SatisfactionRating
    {
        public long? Id { get; set; }

        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }

        [JsonPropertyName("assignee_id")]
        public long AssigneeId { get; set; }

        [JsonPropertyName("requester_id")]
        public long RequesterId { get; set; }

        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("score")]
        public SatisfactionRatingScore Score { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
