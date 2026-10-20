using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/support/organizations#update-organization
    /// </summary>
    public class OrganizationUpdateRequest
    {
        public OrganizationUpdateRequest(Organization organization)
        {
            Organization = organization;
        }

        /// <summary>
        /// The Organization to update
        /// </summary>
        [JsonPropertyName("organization")]
        public Organization Organization { get; set; }
    }
}