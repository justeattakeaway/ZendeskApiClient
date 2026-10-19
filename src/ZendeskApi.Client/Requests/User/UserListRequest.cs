using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests.User
{
    internal class UserListRequest<T>
    {
        public UserListRequest(IEnumerable<T> users)
        {
            Users = users;
        }

        [JsonPropertyName("users")]
        public IEnumerable<T> Users { get; set; }
    }
}