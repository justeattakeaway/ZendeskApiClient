using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Options;

namespace ZendeskApi.Client
{
    public class ZendeskApiClientFactory : IZendeskApiClient
    {
        private readonly ZendeskOptions _options;
        private readonly IHttpClientFactory _httpClientFactory;

        public ZendeskApiClientFactory(
            IOptions<ZendeskOptions> options,
            IHttpClientFactory httpClientFactory)
        {
            _options = options.Value;
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateClient(string resource = null)
        {
            var formattedResource = resource?
                .Trim('/');

            if (!string.IsNullOrEmpty(formattedResource))
                formattedResource = formattedResource + "/";

            var client = _httpClientFactory
                .CreateClient("zendeskApiClient");

            client.BaseAddress = new Uri($"{_options.EndpointUri}/{formattedResource}");

            var authorizationHeader = _options
                .GetAuthorizationHeader();

            // Empty when the client credentials grant is configured, as the OAuth handler sets
            // the header per request instead.
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                client.DefaultRequestHeaders
                    .Add(
                        "Authorization",
                        authorizationHeader);
            }

            client.DefaultRequestHeaders.UserAgent.ParseAdd("justeat.zendesk-api-client-csharp");

            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_options.Timeout != null)
                client.Timeout = _options.Timeout.Value;

            return client;
        }

        public HttpClient CreateServiceStatusClient()
        {
            var client = _httpClientFactory
                .CreateClient();

            client.BaseAddress = new Uri("https://status.zendesk.com");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("justeat.zendesk-api-client-csharp");
            client.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (_options.Timeout != null)
                client.Timeout = _options.Timeout.Value;

            return client;
        }
    }
}
