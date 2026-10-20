using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Exceptions;

namespace ZendeskApi.Client.Extensions
{
    internal static class HttpResponseExtensions
    {
        internal static async Task<HttpResponseMessage> SetToNullWhen(
            this Task<HttpResponseMessage> response,
            HttpStatusCode code)
        {
            var unwrappedResponse = await response;

            if (unwrappedResponse.StatusCode == code)
                return null;

            return unwrappedResponse;
        }

        internal static async Task<HttpResponseMessage> LogInformationWhenNull(
            this Task<HttpResponseMessage> response,
            ILogger logger,
            string message)
        {
            var unwrappedResponse = await response;

            if (unwrappedResponse != null)
                return unwrappedResponse;

            logger.LogInformation(message);

            return null;
        }

        internal static async Task<T> ReadContentAsAsync<T>(
            this Task<HttpResponseMessage> response,
            JsonConverter converter = null)
            where T : class
        {
            var unwrappedResponse = await response;

            if (unwrappedResponse == null)
                return null;

            if (converter == null)
            {
                return await unwrappedResponse
                    .Content
                    .ReadAsAsync<T>();
            }

            return await unwrappedResponse
                .Content
                .ReadAsAsync<T>(converter);
        }

        internal static async Task<HttpResponseMessage> ThrowIfUnsuccessful(
            this Task<HttpResponseMessage> response,
            string helpDocLink,
            HttpStatusCode[] expected,
            string helpDocLinkPrefix = "support")
        {
            var unwrappedResponse = await response;

            if (unwrappedResponse == null)
                return null;

            if (!expected.Contains(unwrappedResponse.StatusCode))
            {
                await unwrappedResponse.ThrowZendeskRequestException(
                    helpDocLink,
                    expected,
                    helpDocLinkPrefix);
            }

            return unwrappedResponse;
        }

        internal static async Task<HttpResponseMessage> ThrowIfUnsuccessful(
            this Task<HttpResponseMessage> response,
            string helpDocLink,
            HttpStatusCode? expected = null,
            string helpDocLinkPrefix = "support")
        {
            var unwrappedResponse = await response;

            if (unwrappedResponse == null)
                return null;

            if (!unwrappedResponse.IsSuccessStatusCode)
            {
                await unwrappedResponse.ThrowZendeskRequestException(
                    helpDocLink,
                    expected,
                    helpDocLinkPrefix);
            }

            return unwrappedResponse;
        }

        internal static async Task<HttpResponseMessage> ThrowIfUnsuccessful(
            this HttpResponseMessage response, 
            string helpDocLink,
            HttpStatusCode? expected = null,
            string helpDocLinkPrefix = "support")
        {
            if (response == null)
                return null;

            if (!response.IsSuccessStatusCode)
            {
                await response.ThrowZendeskRequestException(
                    helpDocLink,
                    expected,
                    helpDocLinkPrefix);
            }

            return response;
        }

        internal static async Task ThrowZendeskRequestException(
            this HttpResponseMessage response, 
            string helpDocLink,
            HttpStatusCode? expected = null,
            string helpDocLinkPrefix = "support")
        {
            if (response == null)
                return;

            await response.ThrowZendeskRequestException(
                helpDocLink,
                expected.HasValue ? new[] {expected.Value} : null,
                helpDocLinkPrefix);
        }

        internal static async Task ThrowZendeskRequestException(
            this HttpResponseMessage response,
            string helpDocLink,
            HttpStatusCode[] expected = null,
            string helpDocLinkPrefix = "support")
        {
            if (response == null)
                return;

            var builder = new ZendeskRequestExceptionBuilder()
                .WithResponse(response)
                .WithHelpDocsLink($"/{helpDocLinkPrefix}/{helpDocLink}");

            if (expected != null)
                builder.WithExpectedHttpStatus(expected);

            throw await builder.Build();
        }
    }
}
