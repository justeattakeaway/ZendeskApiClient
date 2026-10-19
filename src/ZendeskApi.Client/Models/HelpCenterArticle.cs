using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class HelpCenterArticle
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? Created { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("html_url")]
        public string HtmlUrl { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("author_id")]
        public long? AuthorId { get; set; }

        [JsonPropertyName("comments_disabled")]
        public bool? CommentsDisabled { get; set; }

        [JsonPropertyName("outdated")]
        public bool? Outdated { get; set; }

        [JsonPropertyName("label_names")]
        public IEnumerable<string> LabelNames { get; set; }

        [JsonPropertyName("promoted")]
        public bool? Promoted { get; set; }

        [JsonPropertyName("position")]
        public int? Position { get; set; }

        [JsonPropertyName("section_id")]
        public long? SectionId { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("source_locale")]
        public string SourceLocale { get; set; }

        [JsonPropertyName("draft")]
        public bool Draft { get; set; }
    }
}