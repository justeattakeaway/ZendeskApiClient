using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class UserRequest<T>
    {
        [JsonPropertyName("user")]
        public T User { get; }

        public UserRequest(T user)
        {
            User = user;
        }
    }
}