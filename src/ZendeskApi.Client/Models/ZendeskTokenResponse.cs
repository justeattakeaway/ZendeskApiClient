using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    /// <summary>
    /// Response returned by the Zendesk token endpoint. The client credentials grant does not
    /// issue a refresh token.
    /// </summary>
    internal class ZendeskTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Seconds the access token remains valid. Absent for OAuth clients that have no
        /// configured expiry.
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}
