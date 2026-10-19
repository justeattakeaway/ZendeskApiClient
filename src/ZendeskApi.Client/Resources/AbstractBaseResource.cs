using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Extensions;
using ZendeskApi.Client.Formatters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Resources
{
    public abstract class AbstractBaseResource<TResource>
    {
        protected readonly IZendeskApiClient ApiClient;
        protected readonly ILogger Logger;
        protected readonly string DocsResource;

        protected readonly Func<ILogger, string, IDisposable> LoggerScope = LoggerMessage.DefineScope<string>(typeof(TResource).Name + ": {0}");

        protected AbstractBaseResource(
            IZendeskApiClient apiClient, 
            ILogger logger,
            string docsResource)
        {
            ApiClient = apiClient;
            Logger = logger;
            DocsResource = docsResource;
        }

        protected async Task<HttpResponseMessage> ExecuteRequest(
            Func<HttpClient, CancellationToken, Task<HttpResponseMessage>> requestBodyAccessor,
            string scope,
            CancellationToken cancellationToken = default)
        {
            using (LoggerScope(Logger, scope))
            using (var client = ApiClient.CreateClient())
            {
                return await requestBodyAccessor(client, cancellationToken);
            }
        }

        protected async Task<T> GetAsync<T>(
            string resource,
            string docs,
            string scope,
            PagerParameters pager = null,
            JsonConverter converter = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.GetAsync(resource, pager, token).ConfigureAwait(false),
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>(converter);
        }

        protected async Task<T> GetAsync<T>(
            string resource,
            string docs,
            string scope,
            CursorPager pager,
            JsonConverter converter = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(resource, pager, token).ConfigureAwait(false),
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>(converter);
        }

        protected async Task<T> GetAsync<T>(
            string resource,
            string docs,
            string scope,
            CursorPagerVariant pager,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(resource, pager, token).ConfigureAwait(false),
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>();
        }

        protected async Task<T> GetWithNotFoundCheckAsync<T>(
            string resource,
            string docs,
            string scope,
            string notFoundLogMessage,
            PagerParameters pager = null,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await ExecuteRequest(async (client, token) =>
                    await client.GetAsync(resource, pager, token).ConfigureAwait(false), 
                    scope,
                    cancellationToken)
                .SetToNullWhen(HttpStatusCode.NotFound)
                .LogInformationWhenNull(Logger, notFoundLogMessage)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>();
        }

        protected async Task<T> GetWithNotFoundCheckAsync<T>(
            string resource,
            string docs,
            string scope,
            string notFoundLogMessage,
            CursorPager pager,
            CancellationToken cancellationToken = default)
            where T : class
        {
            return await ExecuteRequest(async (client, token) =>
                        await client.GetAsync(resource, pager, token).ConfigureAwait(false),
                    scope,
                    cancellationToken)
                .SetToNullWhen(HttpStatusCode.NotFound)
                .LogInformationWhenNull(Logger, notFoundLogMessage)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<T>();
        }

        protected async Task<HttpResponseMessage> CreateAsync<TRequest>(
            string resource,
            TRequest item,
            string docs,
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created,
            string scope = "CreateAsync",
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.PostAsJsonAsync(resource, item, cancellationToken: token).ConfigureAwait(false), 
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}", expectedStatusCode);
        }

        protected async Task<TResponse> CreateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            HttpStatusCode expectedStatusCode = HttpStatusCode.Created,
            string scope = "CreateAsync",
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            return await CreateAsync<TRequest>(
                    resource,
                    item,
                    docs,
                    expectedStatusCode,
                    scope,
                    cancellationToken)
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> UpdateAsync(
            string resource,
            string docs,
            string scope = "UpdateAsync",
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.PutAsJsonAsync(resource, new StringContent(string.Empty), cancellationToken: cancellationToken).ConfigureAwait(false), 
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}");
        }

        protected async Task<HttpResponseMessage> UpdateAsync(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = "UpdateAsync",
            CancellationToken cancellationToken = default)
        {
            if (ids == null)
                throw new ArgumentNullException($"{nameof(ids)} must not be null", nameof(ids));

            if (ids.Count == 0 || ids.Count > 100)
                throw new ArgumentException($"{nameof(ids)} must have [0..100] elements", nameof(ids));

            var idsAsCsv = ZendeskFormatter.ToCsv(ids);

            return await UpdateAsync(
                $"{resource}?ids={idsAsCsv}",
                docs,
                scope,
                cancellationToken);
        }

        protected async Task<TResponse> UpdateAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string scope = "UpdateAsync",
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.PutAsJsonAsync(resource, item, cancellationToken: token).ConfigureAwait(false), 
                    scope,
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<TResponse> UpdateWithNotFoundCheckAsync<TResponse, TRequest>(
            string resource,
            TRequest item,
            string docs,
            string notFoundLogMessage,
            string scope = "UpdateAsync",
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.PutAsJsonAsync(resource, item, cancellationToken: token).ConfigureAwait(false), 
                    scope,
                    cancellationToken)
                .SetToNullWhen(HttpStatusCode.NotFound)
                .LogInformationWhenNull(Logger, notFoundLogMessage)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}")
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteRequest(async (client, token) => 
                    await client.DeleteAsync(resource, token).ConfigureAwait(false), 
                    scope ?? "DeleteAsync",
                    cancellationToken)
                .ThrowIfUnsuccessful($"{DocsResource}#{docs}", expectedHttpStatusCode);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null,
            CancellationToken cancellationToken = default)
        {
            return await DeleteAsync(
                $"{resource}/{id}",
                docs,
                expectedHttpStatusCode,
                scope ?? $"DeleteAsync{id}",
                cancellationToken);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            long id,
            string docs,
            HttpStatusCode expectedHttpStatusCode = HttpStatusCode.NoContent,
            string scope = null,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            return await DeleteAsync(
                    resource,
                    id,
                    docs,
                    expectedHttpStatusCode,
                    scope,
                    cancellationToken)
                .ReadContentAsAsync<TResponse>();
        }

        protected async Task<HttpResponseMessage> DeleteAsync(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = null,
            CancellationToken cancellationToken = default)
        {
            if (ids == null)
                throw new ArgumentNullException($"{nameof(ids)} must not be null", nameof(ids));

            if (ids.Count == 0 || ids.Count > 100)
                throw new ArgumentException($"{nameof(ids)} must have [0..100] elements", nameof(ids));

            var idsAsCsv = ZendeskFormatter.ToCsv(ids);

            return await DeleteAsync(
                $"{resource}?ids={idsAsCsv}",
                docs,
                HttpStatusCode.OK,
                scope ?? $"DeleteAsync({idsAsCsv})",
                cancellationToken);
        }

        protected async Task<TResponse> DeleteAsync<TResponse>(
            string resource,
            IReadOnlyList<long> ids,
            string docs,
            string scope = null,
            CancellationToken cancellationToken = default)
            where TResponse : class
        {
            return await DeleteAsync(
                    resource,
                    ids,
                    docs,
                    scope,
                    cancellationToken)
                .ReadContentAsAsync<TResponse>();
        }
    }
}