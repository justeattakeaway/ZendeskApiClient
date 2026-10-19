using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public class UserRelatedInformationResponse
    {
        [JsonPropertyName("assigned_tickets")]
        public int AssignedTickets { get; set; }

        [JsonPropertyName("requested_tickets")]
        public int RequestedTickets { get; set; }

        [JsonPropertyName("ccd_tickets")]
        public int CcdTickets { get; set; }

        [JsonPropertyName("organization_subscriptions")]
        public int OrganizationSubscriptions { get; set; }

        [JsonPropertyName("topics")]
        public int Topics { get; set; }

        [JsonPropertyName("topic_comments")]
        public int TopicComments { get; set; }

        [JsonPropertyName("votes")]
        public int Votes { get; set; }

        [JsonPropertyName("subscriptions")]
        public int Subscriptions { get; set; }

        [JsonPropertyName("entry_subscriptions")]
        public int EntrySubscriptions { get; set; }

        [JsonPropertyName("forum_subscriptions")]
        public int ForumSubscriptions { get; set; }
    }
}