using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TicketType
    {
        task,
        incident,
        problem,
        question
    }
}
