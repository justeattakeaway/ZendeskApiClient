using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipCreateRequest
    {
        public OrganizationMembershipCreateRequest(OrganizationMembership organizationMembership)
        {
            OrganizationMembership = organizationMembership;
        }

        /// <summary>
        /// The OrganizationMembership to create
        /// </summary>
        [JsonPropertyName("organization_membership")]
        public OrganizationMembership OrganizationMembership { get; set; }
    }
}