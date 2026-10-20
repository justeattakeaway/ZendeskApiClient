using System;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class OrganizationMembership
    {
        public long? Id { get; set; }

        [JsonPropertyName("user_id")]
        public long? UserId { get; set; }

        [JsonPropertyName("organization_id")]
        public long? OrganizationId { get; set; }

        [JsonPropertyName("default")]
        public bool? Default { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

    }
}
