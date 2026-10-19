using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public class UsersListResponse : PaginationResponse<UserResponse>
    {
        [JsonPropertyName("users")]
        public IEnumerable<UserResponse> Users { get; set; }


        protected override IEnumerable<UserResponse> Enumerable => Users;
    }
}