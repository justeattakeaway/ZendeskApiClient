using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class OrganizationsResponse : PaginationResponse<Organization>
    {
        [JsonPropertyName("organizations")]
        public IEnumerable<Organization> Organizations { get; set; }

        protected override IEnumerable<Organization> Enumerable => Organizations;
    }
}
