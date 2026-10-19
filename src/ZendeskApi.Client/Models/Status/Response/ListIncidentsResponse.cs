using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status.Response;

public class ListIncidentsResponse : DataContainer<IReadOnlyList<Incident>>
{
    [JsonPropertyName("included")]
    public IReadOnlyList<Included.Included> Included { get; set; }
}