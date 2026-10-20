using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status.Included;

public class Relationships
{
    [JsonPropertyName("service")]
    public Service Service { get; set; }
}