using System;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    /// <summary>
    /// <see href="https://developer.zendesk.com/rest_api/docs/core/groups#json-format">Group Response Format</see>
    /// </summary>
    [SearchResultType("group")]
    public class Group : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when creating groups
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The API url of this group
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// The name of the group
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }      
        
        /// <summary>
        /// Deleted groups get marked as such
        /// </summary>
        [JsonPropertyName("deleted")]
        public bool Deleted { get; set; }

        /// <summary>
        /// The time the group was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The time of the last update of the group
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

       
        [JsonPropertyName("result_type")]
        public string ResultType => "group";
    }
}
