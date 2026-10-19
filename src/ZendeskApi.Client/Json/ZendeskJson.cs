using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Json
{
    /// <summary>
    /// The System.Text.Json settings used to read and write all Zendesk API payloads.
    /// </summary>
    public static class ZendeskJson
    {
        public static JsonSerializerOptions Options { get; } = Create();

        /// <summary>
        /// Creates a new copy of the default options, which can be customised without affecting <see cref="Options"/>.
        /// </summary>
        public static JsonSerializerOptions Create()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters =
                {
                    new RuntimeTypeJsonConverterFactory(),
                    new NullAsDefaultJsonConverterFactory(),
                    new JsonStringEnumConverter(),
                    new LenientStringJsonConverter(),
                    new ObjectJsonConverter(),
                    new ObjectDictionaryJsonConverter(),
                    new EnumerableObjectJsonConverterFactory()
                }
            };
        }
    }
}
