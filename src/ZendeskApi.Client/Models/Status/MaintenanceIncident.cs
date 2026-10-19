using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public class MaintenanceIncident
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("attributes")]
    public MaintenanceAttributes Attributes { get; set; }

    [JsonPropertyName("relationships")]
    public Relationships Relationships { get; set; }
}