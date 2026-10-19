using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class ChangeEvent : AuditEvent
    {
        [JsonPropertyName("field_name")]
        public string FieldName { get; set; }
        [JsonPropertyName("value")]
        public object Value { get; set; }
        [JsonPropertyName("previous_value")]
        public object PreviousValue { get; set; }
    }
}