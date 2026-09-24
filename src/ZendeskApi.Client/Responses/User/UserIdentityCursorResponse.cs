using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    [JsonObject]
    public class UserIdentitiesCursorResponse : CursorPaginationResponse<UserIdentity>
    {
        [JsonProperty("identities")]
        public IEnumerable<UserIdentity> Identities { get; set; }

        protected override IEnumerable<UserIdentity> Enumerable => Identities ?? System.Linq.Enumerable.Empty<UserIdentity>();
    }
}
