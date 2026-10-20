using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationMembershipsRequest
    {
        [JsonPropertyName("organization_memberships")]
        public IEnumerable<OrganizationMembership> Item { get; set; }
    }
}
