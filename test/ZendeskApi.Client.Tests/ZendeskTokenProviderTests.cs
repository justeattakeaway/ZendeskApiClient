using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Xunit;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskTokenProviderTests
    {
        private readonly RecordingHttpMessageHandler _handler;
        private readonly IHttpClientFactory _httpClientFactory;

        public ZendeskTokenProviderTests()
        {
            _handler = new RecordingHttpMessageHandler();
            _httpClientFactory = A.Fake<IHttpClientFactory>();

            A.CallTo(() => _httpClientFactory.CreateClient(string.Empty))
                .ReturnsLazily(() => new HttpClient(_handler, disposeHandler: false));
        }

        private ZendeskTokenProvider CreateProvider(ZendeskOptions options = null)
        {
            return new ZendeskTokenProvider(
                _httpClientFactory,
                new OptionsWrapper<ZendeskOptions>(options ?? new ZendeskOptions
                {
                    EndpointUri = "http://kung.fu",
                    ClientId = "CLIENTID",
                    ClientSecret = "SECRET"
                }));
        }

        [Fact]
        public async Task ShouldReturnTheAccessTokenFromTheTokenEndpoint()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            var token = await CreateProvider().GetTokenAsync();

            Assert.Equal("ACCESSTOKEN", token);
        }

        [Fact]
        public async Task ShouldPostToTheTokenEndpointOfTheConfiguredInstance()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            await CreateProvider().GetTokenAsync();

            var request = _handler.Requests.Single();
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("http://kung.fu/oauth/tokens", request.Uri);
        }

        [Fact]
        public async Task ShouldRequestTheClientCredentialsGrantAsJson()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            await CreateProvider(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                ClientId = "CLIENTID",
                ClientSecret = "SECRET",
                Scope = "tickets:read tickets:write",
                TokenExpiresInSeconds = 1800
            }).GetTokenAsync();

            var request = _handler.Requests.Single();
            Assert.Equal("application/json", request.ContentType);

            var body = JObject.Parse(request.Body);
            Assert.Equal("client_credentials", body["grant_type"].Value<string>());
            // Zendesk rejects the grant with "'client_id' required" when this is absent, even
            // though its documentation describes only grant_type and client_secret.
            Assert.Equal("CLIENTID", body["client_id"].Value<string>());
            Assert.Equal("SECRET", body["client_secret"].Value<string>());
            Assert.Equal("tickets:read tickets:write", body["scope"].Value<string>());
            Assert.Equal(1800, body["expires_in"].Value<int>());
        }

        [Fact]
        public async Task ShouldOmitOptionalValuesWhenNotConfigured()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            await CreateProvider().GetTokenAsync();

            var body = JObject.Parse(_handler.Requests.Single().Body);
            Assert.Null(body["scope"]);
            Assert.Null(body["expires_in"]);
        }

        [Fact]
        public async Task ShouldReuseACachedTokenUntilItNearsExpiry()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            var provider = CreateProvider();

            var first = await provider.GetTokenAsync();
            var second = await provider.GetTokenAsync();

            Assert.Equal(first, second);
            Assert.Single(_handler.Requests);
        }

        [Fact]
        public async Task ShouldRequestANewTokenOnceTheCachedOneHasExpired()
        {
            // Expires within the renewal buffer, so the cached token is never considered usable.
            _handler.RespondWith(TokenResponse("FIRST", 30));

            var provider = CreateProvider(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                ClientId = "CLIENTID",
                ClientSecret = "SECRET",
                TokenRenewalBuffer = TimeSpan.FromMinutes(1)
            });

            await provider.GetTokenAsync();

            _handler.RespondWith(TokenResponse("SECOND", 3600));
            var second = await provider.GetTokenAsync();

            Assert.Equal("SECOND", second);
            Assert.Equal(2, _handler.Requests.Count);
        }

        [Fact]
        public async Task ShouldOnlyRequestOneTokenWhenCalledConcurrently()
        {
            _handler.RespondWith(TokenResponse("ACCESSTOKEN", 3600));

            var provider = CreateProvider();

            var tokens = await Task.WhenAll(
                Enumerable
                    .Range(0, 10)
                    .Select(_ => provider.GetTokenAsync()));

            Assert.All(tokens, token => Assert.Equal("ACCESSTOKEN", token));
            Assert.Single(_handler.Requests);
        }

        [Fact]
        public async Task ShouldThrowWhenTheTokenEndpointFails()
        {
            _handler.RespondWith(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"error\":\"invalid_client\"}")
            });

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => CreateProvider().GetTokenAsync());
        }

        [Fact]
        public async Task ShouldThrowWhenTheResponseHasNoAccessToken()
        {
            _handler.RespondWith(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"token_type\":\"bearer\"}")
            });

            await Assert.ThrowsAsync<InvalidOperationException>(
                () => CreateProvider().GetTokenAsync());
        }

        [Fact]
        public async Task ShouldNotLeakTheClientSecretInTheFailureMessage()
        {
            _handler.RespondWith(new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent("{\"error\":\"invalid_client\"}")
            });

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => CreateProvider().GetTokenAsync());

            Assert.DoesNotContain("SECRET", exception.Message);
        }

        private static HttpResponseMessage TokenResponse(string accessToken, int expiresIn)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    $"{{\"access_token\":\"{accessToken}\",\"token_type\":\"bearer\",\"expires_in\":{expiresIn}}}")
            };
        }

        private class RecordingHttpMessageHandler : HttpMessageHandler
        {
            private Func<HttpResponseMessage> _responseFactory;

            public List<RecordedRequest> Requests { get; } = new List<RecordedRequest>();

            public void RespondWith(HttpResponseMessage response)
            {
                _responseFactory = () => response;
            }

            protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
            {
                Requests.Add(new RecordedRequest
                {
                    Method = request.Method,
                    Uri = request.RequestUri.ToString(),
                    ContentType = request.Content?.Headers?.ContentType?.MediaType,
                    Body = request.Content == null
                        ? null
                        : await request.Content.ReadAsStringAsync().ConfigureAwait(false)
                });

                return _responseFactory();
            }
        }

        private class RecordedRequest
        {
            public HttpMethod Method { get; set; }
            public string Uri { get; set; }
            public string ContentType { get; set; }
            public string Body { get; set; }
        }
    }
}
