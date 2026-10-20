using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class Request
    {
        public Request()
        {
            CustomFields = new List<CustomField>();
        }
        
        public long? Id { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }
        
        [JsonPropertyName("url")]
        public Uri Url { get; set; }
        
        [JsonPropertyName("description")]
        public string Description { get; set; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("status")]
        public TicketStatus Status { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("due_at")]
        public DateTime? Due { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("type")]
        public TicketType? Type { get; set; }
        
        [JsonPropertyName("priority")]
        public string Priority { get; set; }

        [JsonPropertyName("comment")]
        public TicketComment Comment { get; set; }

        [JsonPropertyName("requester_id")]
        public long? RequesterId { get; set; }

        [JsonPropertyName("submitter_id")]
        public long? SubmitterId { get; set; }

        [JsonPropertyName("assignee_id")]
        public long? AssigneeId { get; set; }

        [JsonPropertyName("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonPropertyName("collaborator_ids")]
        public List<long> CollaboratorIds { get; set; }

        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        [JsonPropertyName("ticket_form_id")]
        public long? FormId { get; set; }

        [JsonPropertyName("can_be_solved_by_me")]
        public bool CanBeSolvedByMe { get; set; }

        [JsonPropertyName("solved")]
        public bool Solved { get; set; }

        [JsonPropertyName("via")]
        public Via Via { get; set; }

        [JsonPropertyName("custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }
}
