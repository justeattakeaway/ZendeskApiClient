using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SatisfactionRatingResponse
    {
        [JsonPropertyName("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }
    }
}