using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ZendeskApi.Client.Json;

namespace ZendeskApi.Client.Tests.Extensions
{
    public static class HttpResponseExtensions
    {
        private static readonly JsonSerializerOptions Settings = new JsonSerializerOptions(ZendeskJson.Options)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        public static Task WriteAsJson<T>(this HttpResponse response, T value) {
            return response.WriteAsync(JsonSerializer.Serialize(value, Settings));
        }
    }
}
