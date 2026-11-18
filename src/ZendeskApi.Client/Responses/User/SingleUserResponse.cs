using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    class SingleUserResponse
    {
        [JsonProperty("user")]
        public UserResponse UserResponse { get; set; }
    }
}
