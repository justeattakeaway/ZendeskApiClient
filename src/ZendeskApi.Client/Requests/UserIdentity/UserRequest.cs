using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class UserIdentityRequest<T>
    {
        [JsonPropertyName("identity")]
        public T Identity { get; }

        public UserIdentityRequest(T identity)
        {
            Identity = identity;
        }
    }
}