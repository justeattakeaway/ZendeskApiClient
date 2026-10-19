using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models;

public class DataContainer<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; }
}