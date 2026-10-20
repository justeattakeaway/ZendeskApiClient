using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    [SearchResultType("organization")]
    public class Organization : ISearchResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("due_at")]
        public DateTime? Due { get; set; }

        [JsonPropertyName("organization_fields")]
        public Dictionary<object, object> CustomFields { get; set; }
        
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("domain_names")]
        public List<string> DomainNames { get; set; }

        [JsonIgnore]
        [JsonPropertyName("shared_tickets")]
        public bool SharedTickets { get; set; }

        [JsonIgnore]
        [JsonPropertyName("shared_comments")]
        public bool SharedComments { get; set; }

        [JsonIgnore]
        public Uri Url { get; set; }

        [JsonIgnore]
        [JsonPropertyName("group_id")]
        public long GroupId { get; set; }


        [JsonPropertyName("result_type")]
        public string ResultType => "organization";
    }
}
