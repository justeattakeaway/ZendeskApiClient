using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationFieldsReorderRequest
    {
        [JsonPropertyName("organization_field_ids")]
        public long[] OrganizationFieldIds { get; set; }
    }
}