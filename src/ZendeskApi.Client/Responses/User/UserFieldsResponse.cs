using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    class UserFieldsResponse : PaginationResponse<UserField>
    {
        [JsonPropertyName("user_fields")]
        [JsonInclude]
        public IEnumerable<UserField> UserFields { get; internal set; }

        protected override IEnumerable<UserField> Enumerable => UserFields;
    }
}
