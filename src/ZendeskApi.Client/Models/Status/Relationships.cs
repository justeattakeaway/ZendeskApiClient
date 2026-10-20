using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public class Relationships
{
    [JsonPropertyName("incident_services")]
    public IncidentServices IncidentServices { get; set; }

    [JsonPropertyName("incident_updates")]
    public IncidentUpdates IncidentUpdates { get; set; }

    [JsonPropertyName("service")]
    public Service Service { get; set; }
}