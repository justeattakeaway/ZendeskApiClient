using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    class SingleUserRelatedInformationResponse
    {
        [JsonPropertyName("user_related")]
        public UserRelatedInformationResponse UserRelatedInformationResponse { get; set; }
    }
}