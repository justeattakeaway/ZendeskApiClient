using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationFieldCreateUpdateRequest
    {
        public OrganizationFieldCreateUpdateRequest(OrganizationField organizationField)
        {
            OrganizationField = organizationField;
        }
        
        [JsonPropertyName("organization_field")]
        public OrganizationField OrganizationField { get; set; }
    }
}