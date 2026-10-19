using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// See <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#request-parameters">Request body</see> for Create Ticket.
    /// </summary>
    public class TicketCreateRequest
    {
        public TicketCreateRequest()
        {
            
        }

        public TicketCreateRequest(string initialComment, bool initialCommentIsPublic = false)
        {
            Comment = new TicketComment {Body = initialComment, IsPublic = initialCommentIsPublic};    
        }

        public TicketCreateRequest(TicketComment comment)
        {
            Comment = comment;
        }
        
        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("comment")]
        public TicketComment Comment { get; set; }

        [JsonPropertyName("requester_id")]
        public long? RequesterId { get; set; }

        [JsonPropertyName("submitter_id")]
        public long? SubmitterId { get; set; }

        /// <summary>
        /// The numeric ID of the agent to assign the ticket to
        /// </summary>
        [JsonPropertyName("assignee_id")]
        public long? AssigneeId { get; set; }
        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        [JsonPropertyName("collaborator_ids")]
        public IList<long> CollaboratorIds { get; set; }

        [JsonPropertyName("collaborators")]
        public IList<ICollaboratorRequest> Collaborators { get; set; }
            
        [JsonPropertyName("type")]
        public TicketType? Type { get; set; }

        [JsonPropertyName("priority")]
        public Priority? Priority { get; set; }
            
        [JsonPropertyName("status")]
        public TicketStatus? Status { get; set; }
            
        [JsonPropertyName("tags")]
        public IList<string> Tags { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }
            
        [JsonPropertyName("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        [JsonPropertyName("problem_id")]
        public long? ProblemId { get; set; }

        [JsonPropertyName("due_at")]
        public DateTime? Due { get; set; }

        [JsonPropertyName("ticket_form_id")]
        public long? FormId { get; set; }

        [JsonPropertyName("custom_fields")]
        [JsonConverter(typeof(CustomFieldsConverter))]
        public ICustomFields CustomFields { get; set; }

        [JsonPropertyName("via_followup_source_id")]
        public long? ViaFollowupSourceId { get; set; }
            
        [JsonPropertyName("requester")]
        public TicketRequester Requester { get; set; }

        /// <summary>
        /// The organization of the requester. You can only specify the ID of an organization associated with the requester. See <see href="https://developer.zendesk.com/rest_api/docs/core/organization_memberships">Organization Memberships</see>
        /// </summary>
        [JsonPropertyName("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonPropertyName("brand_id")]
        public long? BrandId { get; set; }
    }
}
