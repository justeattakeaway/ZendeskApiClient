using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public class MaintenanceAttributes
{
    [JsonPropertyName("degradation")]
    public bool Degradation { get; set; }

    [JsonPropertyName("impact")]
    public Impact Impact { get; set; }

    [JsonPropertyName("outage")]
    public bool Outage { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("maintenance_article")]
    public string MaintenanceArticle { get; set; }

    [JsonPropertyName("maintenance_end_time")]
    public DateTime MaintenanceEndTime { get; set; }

    [JsonPropertyName("maintenance_start_time")]
    public DateTime MaintenanceStartTime { get; set; }
}