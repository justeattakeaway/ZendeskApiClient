using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class OrganizationField
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }
        
        [JsonPropertyName("url")]
        public Uri Url { get; set; }
        
        [JsonPropertyName("key")]
        public string Key { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("raw_title")]
        public string RawTitle { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonPropertyName("raw_description")]
        public string RawDescription { get; set; }
        
        [JsonPropertyName("position")]
        public int? Position { get; set; }
        
        [JsonPropertyName("active")]
        public bool? Active { get; set; }
        
        [JsonPropertyName("system")]
        public bool? System { get; set; }
        
        [JsonPropertyName("regexp_for_validation")]
        public string RegexpForValidation { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("tag")]
        public string Tag { get; set; }

        [JsonPropertyName("custom_field_options")]
        public List<CustomFieldOption> CustomFieldOptions { get; set; }
    }
}