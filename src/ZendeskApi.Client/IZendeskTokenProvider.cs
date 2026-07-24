using System.Threading;
using System.Threading.Tasks;

namespace ZendeskApi.Client
{
    /// <summary>
    /// Supplies OAuth access tokens for requests to the Zendesk API.
    /// </summary>
    public interface IZendeskTokenProvider
    {
        /// <summary>
        /// Returns a valid access token, obtaining a new one if none is cached or the cached
        /// token has expired.
        /// </summary>
        Task<string> GetTokenAsync(CancellationToken cancellationToken = default);
    }
}
