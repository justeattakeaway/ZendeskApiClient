using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class FollowerChangeEvent : AuditEvent
    {
        [JsonPropertyName("previous_followers")]
        public string[] PreviousFollowers { get; set; }
        [JsonPropertyName("current_followers")]
        public string[] CurrentFollowers { get; set; }
    }
}