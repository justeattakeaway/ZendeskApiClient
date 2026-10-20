using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentityResponse<T>
    {
        [JsonPropertyName("identity")]
        public T Identity { get; set; }
    }
}