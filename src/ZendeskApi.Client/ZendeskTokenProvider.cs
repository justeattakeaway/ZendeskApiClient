using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client
{
    /// <summary>
    /// Obtains access tokens using the OAuth client credentials grant, caching each token until
    /// shortly before it expires. The grant does not issue refresh tokens, so an expired token is
    /// replaced by running the grant again.
    /// </summary>
    public class ZendeskTokenProvider : IZendeskTokenProvider, IDisposable
    {
        private const string TokenResource = "oauth/tokens";
        private const string GrantType = "client_credentials";

        /// <summary>
        /// Lifetime assumed when Zendesk does not return one. OAuth clients created before
        /// 30 April 2026 have no default expiry, but tokens are still re-requested periodically
        /// rather than cached indefinitely.
        /// </summary>
        private static readonly TimeSpan FallbackLifetime = TimeSpan.FromHours(24);

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ZendeskOptions _options;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private string _accessToken;
        private DateTimeOffset _expiresAt;

        public ZendeskTokenProvider(
            IHttpClientFactory httpClientFactory,
            IOptions<ZendeskOptions> options)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
        {
            if (TryGetCachedToken(out var cachedToken))
                return cachedToken;

            await _semaphore
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);

            try
            {
                // Another caller may have obtained a token while this one waited on the lock.
                if (TryGetCachedToken(out cachedToken))
                    return cachedToken;

                var response = await RequestTokenAsync(cancellationToken)
                    .ConfigureAwait(false);

                var lifetime = response.ExpiresIn > 0
                    ? TimeSpan.FromSeconds(response.ExpiresIn)
                    : FallbackLifetime;

                _accessToken = response.AccessToken;
                _expiresAt = DateTimeOffset.UtcNow.Add(lifetime);

                return _accessToken;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private bool TryGetCachedToken(out string token)
        {
            token = _accessToken;

            return !string.IsNullOrEmpty(token)
                && DateTimeOffset.UtcNow < _expiresAt.Subtract(_options.TokenRenewalBuffer);
        }

        private async Task<ZendeskTokenResponse> RequestTokenAsync(CancellationToken cancellationToken)
        {
            // Deliberately not the "zendeskApiClient" named client: that pipeline carries the
            // handler which calls back into this provider, which would recurse.
            var client = _httpClientFactory.CreateClient();

            var payload = JsonConvert.SerializeObject(
                new ZendeskTokenRequest
                {
                    GrantType = GrantType,
                    ClientId = _options.ClientId,
                    ClientSecret = _options.ClientSecret,
                    Scope = string.IsNullOrWhiteSpace(_options.Scope) ? null : _options.Scope,
                    ExpiresIn = _options.TokenExpiresInSeconds
                },
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            using (var content = new StringContent(payload, Encoding.UTF8, "application/json"))
            {
                var requestUri = $"{_options.EndpointUri?.TrimEnd('/')}/{TokenResource}";

                var response = await client
                    .PostAsync(requestUri, content, cancellationToken)
                    .ConfigureAwait(false);

                var body = await response.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException(
                        "Unable to obtain a Zendesk access token using the client credentials grant. " +
                        $"The token endpoint responded with {(int)response.StatusCode}{DescribeError(body)}.");
                }

                var token = JsonConvert.DeserializeObject<ZendeskTokenResponse>(body);

                if (token == null || string.IsNullOrWhiteSpace(token.AccessToken))
                {
                    throw new InvalidOperationException(
                        "The Zendesk token endpoint returned a response without an access token.");
                }

                return token;
            }
        }

        /// <summary>
        /// Describes an OAuth error response for diagnostics. Only the documented "error" and
        /// "error_description" values are included; the body is never surfaced wholesale, so a
        /// request echoed back by the endpoint cannot leak the client secret into logs.
        /// </summary>
        private static string DescribeError(string body)
        {
            if (string.IsNullOrWhiteSpace(body))
                return string.Empty;

            try
            {
                var error = JsonConvert.DeserializeObject<ZendeskTokenErrorResponse>(body);

                if (error == null || string.IsNullOrWhiteSpace(error.Error))
                    return string.Empty;

                return string.IsNullOrWhiteSpace(error.Description)
                    ? $" ({error.Error})"
                    : $" ({error.Error}: {error.Description})";
            }
            catch (JsonException)
            {
                return string.Empty;
            }
        }

        public void Dispose()
        {
            _semaphore.Dispose();
        }
    }
}
