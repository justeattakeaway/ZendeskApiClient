using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class LocaleListResponse
    {
        [JsonPropertyName("locales")]
        public Locale[] Locales { get; set; }
    }

    public class LocaleResponse
    {
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }
    }

    public class Locale
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? Created { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("default")]
        public bool Default { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("native_name")]
        public string NativeName { get; set; }

        [JsonPropertyName("presentation_name")]
        public string PresentationName { get; set; }

        [JsonPropertyName("locale")]
        public string Value { get; set; }

        [JsonPropertyName("rtl")]
        public bool Rtl { get; set; }
    }
}
