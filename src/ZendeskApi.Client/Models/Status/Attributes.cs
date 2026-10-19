using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status
{
    public class Attributes
    {
        [JsonPropertyName("degradation")]
        public bool Degradation { get; set; }

        [JsonPropertyName("impact")]
        public Impact Impact { get; set; }

        [JsonPropertyName("outage")]
        public bool Outage { get; set; }

        [JsonPropertyName("postmortem")]
        public string Postmortem { get; set; }

        [JsonPropertyName("resolved_at")]
        public DateTime? ResolvedAt { get; set; }

        [JsonPropertyName("started_at")]
        public DateTime StartedAt { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("incident_id")]
        public string IncidentId { get; set; }

        [JsonPropertyName("service_id")]
        public string ServiceId { get; set; }
    }
}