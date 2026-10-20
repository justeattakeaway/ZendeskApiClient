using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests.User
{
    public class UserFieldCreateUpdateRequest
    {
        public UserFieldCreateUpdateRequest(UserField userField)
        {
            UserField = userField;
        }

        [JsonPropertyName("user_field")]
        public UserField UserField { get; set; }
    }
}
