using System;

namespace ZendeskApi.Client.Options
{
    public class ZendeskOptions
    {
        public string EndpointUri { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string OAuthToken { get; set; }
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// The Unique identifier of an OAuth client, used with <see cref="ClientSecret"/> for the
        /// client credentials grant.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// The Secret of an OAuth client. When set, access tokens are obtained automatically
        /// using the OAuth client credentials grant and renewed as they expire.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Space separated scopes requested for tokens obtained via the client credentials
        /// grant, for example "tickets:read tickets:write". Optional.
        /// </summary>
        public string Scope { get; set; }

        /// <summary>
        /// How long, in seconds, tokens obtained via the client credentials grant remain valid.
        /// Optional; when unset Zendesk applies its own default.
        /// </summary>
        public int? TokenExpiresInSeconds { get; set; }

        /// <summary>
        /// How long before expiry a cached token is renewed, guarding against a token lapsing
        /// in flight. Defaults to one minute.
        /// </summary>
        public TimeSpan TokenRenewalBuffer { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        /// True when the client credentials grant is configured and should be used to obtain
        /// access tokens.
        /// </summary>
        public bool UsesClientCredentials => !string.IsNullOrWhiteSpace(ClientSecret);
    }
}
