using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ZendeskApi.Client.Json;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpRequestExtensions
    {
        private static StringContent ToJsonContent<T>(T value, JsonSerializerOptions options)
        {
            // Serialize using the runtime type so derived types keep all of their properties
            var json = JsonSerializer.Serialize(value, value?.GetType() ?? typeof(T), options ?? ZendeskJson.Options);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = ToJsonContent(value, options);

            return await client.PutAsync(
                    requestUri, 
                    content, 
                    cancellationToken)
                .ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(
            this HttpClient client,
            string requestUri,
            T value,
            JsonSerializerOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = ToJsonContent(value, options);

            return await client.PostAsync(
                    requestUri, 
                    content,
                    cancellationToken)
                .ConfigureAwait(false);
        }

        /**
         * See https://developer.zendesk.com/rest_api/docs/core/attachments#upload-files
         * The api takes binary files without Multipart boundaries
         */
        public static async Task<HttpResponseMessage> PostAsBinaryAsync(
            this HttpClient client,
            string requestUri,
            Stream inputStream,
            string fileName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var stream = new MemoryStream())
            {
                inputStream.CopyTo(stream);

                using (var content = new ByteArrayContent(stream.ToArray()))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/binary");

                    return await client.PostAsync(
                            requestUri, 
                            content, 
                            cancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }

    }
}