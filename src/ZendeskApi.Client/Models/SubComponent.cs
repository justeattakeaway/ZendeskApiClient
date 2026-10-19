using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class SubComponentResponse
    {
        [JsonPropertyName("subcomponents")]
        public SubComponent[] SubComponents { get; set; }
    }

    public class SubComponent
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("component_id")]
        public int? ComponentId { get; set; }

        [JsonPropertyName("pod_id")]
        public int PodId { get; set; }
    }
}