using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Models
{
    [JsonObject("user_fields")]
    public class UserField
    {
        [JsonProperty]
        public long? Id { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("raw_title")]
        public string RawTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("raw_description")]
        public string RawDescription { get; set; }

        [JsonProperty("position")]
        public long? Position { get; set; }

        [JsonProperty("active")]
        public bool? Active { get; set; }

        [JsonProperty("system")]
        public bool? System { get; set; }

        [JsonProperty("regexp_for_validation")]
        public string RegexpForValidation { get; set; }

        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("custom_field_options")]
        public List<CustomFieldOption> CustomFieldOptions { get; set; }
    }
}
