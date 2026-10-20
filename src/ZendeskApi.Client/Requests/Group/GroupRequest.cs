using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Requests
{
    internal class GroupRequest<T>
    {
        [JsonPropertyName("group")]
        public T Group { get; set; }

        public GroupRequest(T group)
        {
            Group = group;
        }
    }
}