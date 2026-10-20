using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.AuditEvents
{
    public class EmailCCChangeEvent : AuditEvent
    {
        [JsonPropertyName("previous_email_ccs")]
        public string[] PreviousEmailCCs { get; set; }
        [JsonPropertyName("current_email_ccs")]
        public string[] CurrentEmailCCs { get; set; }
    }
}