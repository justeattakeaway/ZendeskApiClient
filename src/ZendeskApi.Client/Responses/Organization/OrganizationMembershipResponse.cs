using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationMembershipResponse
    {
        [JsonPropertyName("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}