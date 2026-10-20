using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class TicketForm
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("raw_name")]
        public string RawName { get; set; }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        [JsonPropertyName("raw_display_name")]
        public string RawDisplayName { get; set; }

        [JsonPropertyName("position")]
        public long? Position { get; set; }

        [JsonPropertyName("end_user_visible")]
        public bool? EndUserVisible { get; set; }

        [JsonPropertyName("ticket_field_ids")]
        public List<long> TicketFieldIds { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        [JsonPropertyName("default")]
        public bool? Default { get; set; }

    }
}
