using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    class OrganizationFieldsResponse : PaginationResponse<OrganizationField>
    {
        [JsonPropertyName("organization_fields")]
        [JsonInclude]
        public IEnumerable<OrganizationField> OrganizationFields { get; internal set; }

        protected override IEnumerable<OrganizationField> Enumerable => OrganizationFields;
    }
}
