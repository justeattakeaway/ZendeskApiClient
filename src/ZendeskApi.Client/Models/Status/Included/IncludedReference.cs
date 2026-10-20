using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status.Included;

public class IncludedReference
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public Type Type { get; set; }
}