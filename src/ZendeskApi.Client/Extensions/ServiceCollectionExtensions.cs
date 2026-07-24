using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Options;
using ZendeskApi.Client.Pagination;
#pragma warning disable 618

namespace ZendeskApi.Client.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string username,
            string token)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClient>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.Username = username;
                options.Token = token;
            });

            return services;
        }

        public static IServiceCollection AddZendeskClientWithHttpClientFactory(this IServiceCollection services,
            string endpointUri,
            string username,
            string token,
            Action<HttpClient> configureClient = null)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClientFactory>();
            services.AddScoped<ICursorPaginatedIteratorFactory, CursorPaginatedIteratorFactory>();

            services.AddHttpClient("zendeskApiClient", c =>
            {
                configureClient?.Invoke(c);
            });

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.Username = username;
                options.Token = token;
            });

            return services;
        }

        public static IServiceCollection AddZendeskClient(this IServiceCollection services,
            string endpointUri,
            string oAuthToken)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClient>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.OAuthToken = oAuthToken;
            });

            return services;
        }

        public static IServiceCollection AddZendeskClientWithHttpClientFactory(this IServiceCollection services,
            string endpointUri,
            string oAuthToken,
            Action<HttpClient> configureClient = null)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClientFactory>();

            services.AddHttpClient("zendeskApiClient", c =>
            {
                configureClient?.Invoke(c);
            });

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.OAuthToken = oAuthToken;
            });

            return services;
        }

        /// <summary>
        /// Registers the Zendesk client using the OAuth client credentials grant. Access tokens
        /// are obtained from the client secret and renewed automatically as they expire, so no
        /// token needs to be supplied or rotated by the caller.
        /// </summary>
        /// <param name="endpointUri">The Zendesk instance, for example https://example.zendesk.com.</param>
        /// <param name="clientId">The Unique identifier of an OAuth client.</param>
        /// <param name="clientSecret">The Secret of an OAuth client.</param>
        /// <param name="scope">Optional space separated scopes, for example "tickets:read tickets:write".</param>
        /// <param name="configureClient">Optional additional configuration of the underlying client.</param>
        public static IServiceCollection AddZendeskClientWithClientCredentials(this IServiceCollection services,
            string endpointUri,
            string clientId,
            string clientSecret,
            string scope = null,
            Action<HttpClient> configureClient = null)
        {
            services.AddScoped<IZendeskClient, ZendeskClient>();
            services.AddScoped<IZendeskApiClient, ZendeskApiClientFactory>();
            services.AddScoped<ICursorPaginatedIteratorFactory, CursorPaginatedIteratorFactory>();

            // Singleton so that one cached token is shared by every caller in the process.
            services.AddSingleton<IZendeskTokenProvider, ZendeskTokenProvider>();
            services.AddTransient<ZendeskOAuthDelegatingHandler>();

            services
                .AddHttpClient("zendeskApiClient", c =>
                {
                    configureClient?.Invoke(c);
                })
                .AddHttpMessageHandler<ZendeskOAuthDelegatingHandler>();

            services.Configure<ZendeskOptions>(options => {
                options.EndpointUri = endpointUri;
                options.ClientId = clientId;
                options.ClientSecret = clientSecret;
                options.Scope = scope;
            });

            return services;
        }
    }
}
