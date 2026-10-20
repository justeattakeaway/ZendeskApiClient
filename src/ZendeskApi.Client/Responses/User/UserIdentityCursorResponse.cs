using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class UserIdentitiesCursorResponse : CursorPaginationResponse<UserIdentity>
    {
        [JsonPropertyName("identities")]
        public IEnumerable<UserIdentity> Identities { get; set; }

        protected override IEnumerable<UserIdentity> Enumerable => Identities ?? System.Linq.Enumerable.Empty<UserIdentity>();
    }
}
