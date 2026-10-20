using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationFieldResponse
    {
        [JsonPropertyName("organization_field")]
        public OrganizationField OrganizationField { get; set; }
    }
}