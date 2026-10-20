using System.Text.Json.Serialization;
namespace ZendeskApi.Client.Models
{
    public class Tag 
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
