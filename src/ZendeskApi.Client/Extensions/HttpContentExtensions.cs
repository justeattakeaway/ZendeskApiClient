using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZendeskApi.Client.Json;

namespace ZendeskApi.Client.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsAsync<T>(this HttpContent content, JsonConverter jsonConverter)
        {
            var options = new JsonSerializerOptions(ZendeskJson.Options);
            options.Converters.Insert(0, jsonConverter);

            using (var data = await content.ReadAsStreamAsync())
            {
                return data.ReadAs<T>(options);
            }
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            using (var data = await content.ReadAsStreamAsync()) 
            {
                return data.ReadAs<T>();
            }
        }

        public static T ReadAs<T>(this Stream stream)
        {
            return stream.ReadAs<T>(ZendeskJson.Options);
        }

        private static T ReadAs<T>(this Stream stream, JsonSerializerOptions options)
        {
            using (var buffer = new MemoryStream())
            {
                stream.CopyTo(buffer);

                if (buffer.Length == 0)
                {
                    return default;
                }

                return JsonSerializer.Deserialize<T>(buffer.ToArray(), options);
            }
        }
    }
}
