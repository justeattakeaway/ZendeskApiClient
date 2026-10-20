using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterLocales
    {
        [JsonPropertyName("default_locale")]
        public string DefaultLocale { get; set; }

        [JsonPropertyName("locales")]
        public string[] Locales { get; set; }
    }
}
