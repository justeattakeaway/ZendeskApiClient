using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    /// <summary>
    /// Error returned by the Zendesk token endpoint, as described by the OAuth specification.
    /// </summary>
    internal class ZendeskTokenErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("error_description")]
        public string Description { get; set; }
    }
}
