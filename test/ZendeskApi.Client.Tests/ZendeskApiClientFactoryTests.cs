using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Xunit;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client.Tests
{
    public class ZendeskApiClientFactoryTests
    {
        private const string Authorization = "Authorization";

        private readonly ZendeskOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public ZendeskApiClientFactoryTests()
        {
            _options = new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                Username = "testuser@gmail.com",
                Token = "TESTTOKEN"
            };

            _httpClientFactory = A.Fake<IHttpClientFactory>();

            A.CallTo(() => _httpClientFactory.CreateClient("zendeskApiClient"))
                .Returns(new HttpClient());
        }

        [Fact]
        public void ShouldDefaultBaseResourceAsEndpointUri()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);
            var httpClient = client.CreateClient();

            Assert.Equal("http://kung.fu/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUri()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);
            var httpClient = client.CreateClient("Bruce/Li");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldAppendResourceOnEndOfBaseResourceAsEndpointUriWithWeirdSlashes()
        {
            var options = new OptionsWrapper<ZendeskOptions>(_options);
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);
            var httpClient = client.CreateClient("/Bruce/Li/");

            Assert.Equal("http://kung.fu/Bruce/Li/", httpClient.BaseAddress.ToString());
        }

        [Fact]
        public void ShouldCreateBasicAuthorizationHeader()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                Username = "testuser@gmail.com",
                Token = "TESTTOKEN"
            });
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);

            var httpClient = client.CreateClient();

            var authHeader = httpClient.DefaultRequestHeaders.GetValues(Authorization).FirstOrDefault();
            var expectedHeader = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Username}/token:{_options.Token}"))}";
            Assert.Equal(authHeader, expectedHeader);
        }

        [Fact]
        public void ShouldCreateBearerAuthorizationHeader()
        {
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                OAuthToken = "TESTTOKEN"
            });
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);

            var httpClient = client.CreateClient();

            var authHeader = httpClient.DefaultRequestHeaders.GetValues(Authorization).FirstOrDefault();
            var expectedHeader = $"Bearer {options.Value.OAuthToken}";
            Assert.Equal(authHeader, expectedHeader);
        }

        [Fact]
        public void ShouldNotSetAnAuthorizationHeaderWhenUsingClientCredentials()
        {
            // The OAuth handler sets the header per request instead, so that a token renewed
            // after expiry is used without the client being recreated.
            var options = new OptionsWrapper<ZendeskOptions>(new ZendeskOptions
            {
                EndpointUri = "http://kung.fu",
                ClientSecret = "SECRET"
            });
            var client = new ZendeskApiClientFactory(options, _httpClientFactory);

            var httpClient = client.CreateClient();

            Assert.False(httpClient.DefaultRequestHeaders.Contains(Authorization));
        }
    }
}
