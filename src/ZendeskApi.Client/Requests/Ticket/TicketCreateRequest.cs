using System;
using System.Collections.Generic;
using Newtonsoft.Json;
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
        
        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("comment")]
        public TicketComment Comment { get; set; }

        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty("submitter_id")]
        public long? SubmitterId { get; set; }

        /// <summary>
        /// The numeric ID of the agent to assign the ticket to
        /// </summary>
        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }
        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        [JsonProperty("collaborator_ids")]
        public IList<long> CollaboratorIds { get; set; }

        [JsonProperty("collaborators")]
        public IList<ICollaboratorRequest> Collaborators { get; set; }
            
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        [JsonProperty("priority")]
        public Priority? Priority { get; set; }
            
        [JsonProperty("status")]
        public TicketStatus? Status { get; set; }
            
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        [JsonProperty("external_id")]
        public string ExternalId { get; set; }
            
        [JsonProperty("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        [JsonProperty("problem_id")]
        public long? ProblemId { get; set; }

        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }

        [JsonProperty("custom_fields")]
        [JsonConverter(typeof(CustomFieldsConverter))]
        public ICustomFields CustomFields { get; set; }

        [JsonProperty("via_followup_source_id")]
        public long? ViaFollowupSourceId { get; set; }
            
        [JsonProperty("requester")]
        public TicketRequester Requester { get; set; }

        /// <summary>
        /// The organization of the requester. You can only specify the ID of an organization associated with the requester. See <see href="https://developer.zendesk.com/rest_api/docs/core/organization_memberships">Organization Memberships</see>
        /// </summary>
        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty("brand_id")]
        public long? BrandId { get; set; }
        
        /// <summary>
        /// Email cc to send notifications to on ticket creations or updates, based on trigger settings. See <see href="https://developer.zendesk.com/documentation/ticketing/managing-tickets/creating-and-updating-tickets/#setting-email-ccs">Email Ccs</see>.  
        /// </summary>
        [JsonProperty("email_ccs")]
        public IList<TicketEmailCc> EmailCcs { get; set; }
    }
}
