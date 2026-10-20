using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    public class OrganizationListRequest<T>
    {
        public OrganizationListRequest(IEnumerable<T> organizations)
        {
            Organizations = organizations;
        }

        [JsonPropertyName("organizations")]
        public IEnumerable<T> Organizations { get; set; }
    }
}