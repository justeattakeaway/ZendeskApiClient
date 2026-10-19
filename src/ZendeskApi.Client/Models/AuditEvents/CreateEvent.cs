using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class CreateEvent : AuditEvent
    {
        [JsonPropertyName("field_name")]
        public string FieldName { get; set; }
        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}