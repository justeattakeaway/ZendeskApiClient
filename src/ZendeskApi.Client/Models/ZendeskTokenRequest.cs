using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    /// <summary>
    /// Request body sent to the Zendesk token endpoint for the client credentials grant.
    /// </summary>
    internal class ZendeskTokenRequest
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int? ExpiresIn { get; set; }
    }
}
