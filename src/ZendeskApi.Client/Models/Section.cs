using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterSection
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? Created { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("outdated")]
        public bool? Outdated { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("category_id")]
        public long? CategoryId { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("source_locale")]
        public string SourceLocale { get; set; }

        [JsonPropertyName("user_segment_id")]
        public long? UserSegmentId { get; set; }

        [JsonPropertyName("parent_section_id")]
        public long? ParentSectionId { get; set; }

        [JsonPropertyName("manageable_by")]
        public string ManageableBy { get; set; }
    }
}
