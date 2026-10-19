using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status.Included;

public class Included
{
    [JsonPropertyName("attributes")]
    public Attributes Attributes { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public Type Type { get; set; }

    [JsonPropertyName("relationships")]
    public Relationships Relationships { get; set; }
}