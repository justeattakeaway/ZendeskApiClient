using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SatisfactionRatingScore
    {
        offered,
        unoffered,
        good,
        bad
    }
}
