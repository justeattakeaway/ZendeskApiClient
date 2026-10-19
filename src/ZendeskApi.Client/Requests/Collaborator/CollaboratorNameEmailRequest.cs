using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    public class CollaboratorNameEmailRequest : ICollaboratorRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public CollaboratorNameEmailRequest(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}