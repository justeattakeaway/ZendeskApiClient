using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public interface IAuditEvent
    {
        [JsonPropertyName("id")]
        long Id { get; set; }
        [JsonPropertyName("type")]
        AuditTypes Type { get; set; }
    }
}