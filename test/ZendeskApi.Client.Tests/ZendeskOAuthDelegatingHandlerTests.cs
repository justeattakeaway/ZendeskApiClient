using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Xunit;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskOAuthDelegatingHandlerTests
    {
        private const string Authorization = "Authorization";

        private readonly IZendeskTokenProvider _tokenProvider;
        private readonly CapturingHttpMessageHandler _inner;

        public ZendeskOAuthDelegatingHandlerTests()
        {
            _tokenProvider = A.Fake<IZendeskTokenProvider>();
            _inner = new CapturingHttpMessageHandler();

            A.CallTo(() => _tokenProvider.GetTokenAsync(A<CancellationToken>._))
                .Returns(Task.FromResult("ACCESSTOKEN"));
        }

        private HttpClient CreateClient()
        {
            var handler = new ZendeskOAuthDelegatingHandler(_tokenProvider)
            {
                InnerHandler = _inner
            };

            return new HttpClient(handler);
        }

        [Fact]
        public async Task ShouldSetTheBearerAuthorizationHeaderFromTheTokenProvider()
        {
            await CreateClient().GetAsync("http://kung.fu");

            Assert.Equal("Bearer ACCESSTOKEN", _inner.Request.Headers.Authorization.ToString());
        }

        [Fact]
        public async Task ShouldReplaceAnAuthorizationHeaderAlreadyOnTheRequest()
        {
            var client = CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", "SHOULDBEREPLACED");

            await client.GetAsync("http://kung.fu");

            var headers = _inner.Request.Headers.GetValues(Authorization).ToList();

            Assert.Single(headers);
            Assert.Equal("Bearer ACCESSTOKEN", headers[0]);
        }

        [Fact]
        public async Task ShouldRequestATokenForEveryRequestSoRenewalsArePickedUp()
        {
            var client = CreateClient();

            await client.GetAsync("http://kung.fu");
            await client.GetAsync("http://kung.fu");

            A.CallTo(() => _tokenProvider.GetTokenAsync(A<CancellationToken>._))
                .MustHaveHappenedTwiceExactly();
        }

        private class CapturingHttpMessageHandler : HttpMessageHandler
        {
            public HttpRequestMessage Request { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Request = request;

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            }
        }
    }
}
