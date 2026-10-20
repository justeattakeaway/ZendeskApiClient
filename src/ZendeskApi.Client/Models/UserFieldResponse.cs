using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class UserFieldResponse
    {
        [JsonPropertyName("user_field")]
        public UserField UserField { get; set; }
    }
}