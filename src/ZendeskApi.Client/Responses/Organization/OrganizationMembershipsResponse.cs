using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationMembershipsResponse : PaginationResponse<OrganizationMembership>
    {
        [JsonPropertyName("organization_memberships")]
        [JsonInclude]
        public IEnumerable<OrganizationMembership> OrganizationMemberships { get; internal set; }
        
        protected override IEnumerable<OrganizationMembership> Enumerable => OrganizationMemberships;
    }
}
