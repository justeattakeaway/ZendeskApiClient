using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ZendeskApi.Client
{
    /// <summary>
    /// Sets the Authorization header on each request from <see cref="IZendeskTokenProvider"/>.
    /// Applied per request rather than per client so that a token replaced after expiry is
    /// picked up without recreating the client.
    /// </summary>
    public class ZendeskOAuthDelegatingHandler : DelegatingHandler
    {
        private const string Scheme = "Bearer";

        private readonly IZendeskTokenProvider _tokenProvider;

        public ZendeskOAuthDelegatingHandler(IZendeskTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _tokenProvider
                .GetTokenAsync(cancellationToken)
                .ConfigureAwait(false);

            // Any header set on the client's default headers is replaced, not appended to.
            request.Headers.Remove("Authorization");
            request.Headers.Authorization = new AuthenticationHeaderValue(Scheme, token);

            return await base
                .SendAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
