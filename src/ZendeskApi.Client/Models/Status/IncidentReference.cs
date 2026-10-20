using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public class IncidentReference
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}