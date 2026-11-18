using Newtonsoft.Json;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CreateEvent : AuditEvent
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }
        [JsonProperty("value")]
        public object Value { get; set; }
    }
}