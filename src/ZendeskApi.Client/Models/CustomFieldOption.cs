using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    [Description("custom_field_option")]
    public class CustomFieldOption
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("raw_name")]
        public string RawName { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
