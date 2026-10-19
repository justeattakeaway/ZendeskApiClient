using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ZendeskApi.Client.Json;

namespace ZendeskApi.Client.Tests.Extensions
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> ReadAsync<T>(this HttpRequest request)
        {
            using var streamReader = new StreamReader(request.Body, Encoding.UTF8);
            {
                var body = await streamReader.ReadToEndAsync();

                return JsonSerializer.Deserialize<T>(body, ZendeskJson.Options);
            }
        }
    }
}
