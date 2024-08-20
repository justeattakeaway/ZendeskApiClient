using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Requests
{
    /// <summary>
    /// <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#request-body">Request body</see> <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#create-ticket">Create Ticket</see>.
    /// </summary>
    public class TicketUpdateRequest
    {
        public TicketUpdateRequest(long id)
        {
            Id = id;
        }

        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// The subject of the ticket
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// An object that adds a comment to the ticket. See <see href="https://developer.zendesk.com/rest_api/docs/core/ticket_comments">Ticket comments</see>. 
        /// To include an attachment with the comment, see Attaching files
        /// </summary>
        [JsonProperty("comment", Required = Required.DisallowNull)]
        public TicketComment Comment { get; set; }

        /// <summary>
        /// The numeric ID of the user asking for support through the ticket
        /// </summary>
        [JsonProperty("requester_id")]
        public long? RequesterId { get; set; }

        /// <summary>
        /// The numeric ID of the agent to assign the ticket to
        /// </summary>
        [JsonProperty("assignee_id")]
        public long? AssigneeId { get; set; }

        /// <summary>
        /// The email address of the agent to assign the ticket to
        /// </summary>
        [JsonProperty("assignee_email")]
        public string AssigneeEmail { get; set; }

        /// <summary>
        /// The numeric ID of the group to assign the ticket to
        /// </summary>
        [JsonProperty("group_id")]
        public long? GroupId { get; set; }

        /// <summary>
        /// An array of the numeric IDs of agents or end-users to CC. Note that this replaces any existing collaborators. An email notification is sent to them when the ticket is created
        /// </summary>
        [JsonProperty("collaborator_ids")]
        public IList<long> CollaboratorIds { get; set; }

        /// <summary>
        /// An array of numeric IDs, emails, or objects containing <c>name</c> and <c>email</c> properties. See <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#setting-collaborators">Setting Collaborators</see>. An email notification is sent to them when the ticket is updated
        /// </summary>
        [JsonProperty("collaborators")]
        public IList<ICollaboratorRequest> Collaborators { get; set; }

        /// <summary>
        /// An array of numeric IDs, emails, or objects containing <c>name</c> and <c>email</c> properties. See <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#setting-collaborators">Setting Collaborators</see>. An email notification is sent to them when the ticket is updated
        /// </summary>
        [JsonProperty("additional_collaborators")]
        public IList<ICollaboratorRequest> AdditionalCollaborators { get; set; }

        /// <summary>
        /// Allowed values are <c>problem</c>, <c>incident</c>, <c>question</c>, or <c>task</c>
        /// </summary>
        [JsonProperty("type")]
        public TicketType? Type { get; set; }

        /// <summary>
        /// Allowed values are <c>urgent</c>, <c>high</c>, <c>normal</c>, or <c>low</c>
        /// </summary>
        [JsonProperty("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// Allowed values are <c>open</c>, <c>pending</c>, <c>hold</c>, <c>solved</c> or <c>closed</c>
        /// </summary>
        [JsonProperty("status")]
        public TicketStatus? Status { get; set; }

        /// <summary>
        /// An array of tags to add to the ticket. Note that the tags replace any existing tags. To keep existing tags, see <see href="https://developer.zendesk.com/rest_api/docs/core/tickets#updating-tag-lists">Updating tag lists</see>
        /// </summary>
        [JsonProperty("tags")]
        public IList<string> Tags { get; set; }

        /// <summary>
        /// An ID to link tickets to local records
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        /// <summary>
        /// For tickets of type "incident", the numeric ID of the problem the incident is linked to, if any
        /// </summary>
        [JsonProperty("problem_id")]
        public long? ProblemId { get; set; }

        /// <summary>
        /// For tickets of type "task", the due date of the task. Accepts the ISO 8601 date format (yyyy-mm-dd)
        /// </summary>
        [JsonProperty("due_at")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// An array of the custom field objects consisting of ids and values. Any tags defined with the custom field replace existing tags
        /// </summary>
        [JsonProperty("custom_fields")]
        [JsonConverter(typeof(CustomFieldsConverter))]
        public ICustomFields CustomFields { get; set; }

        /// <summary>
        /// Datetime of last update received from API. See <c>SafeUpdate</c> param
        /// </summary>
        [JsonProperty("updated_stamp")]
        public DateTime? UpatedStamp { get; set; }

        /// <summary>
        /// Optional boolean. Prevents updates with outdated ticket data (<c>UpatedStamp</c> param required when true)
        /// </summary>
        [JsonProperty("safe_update")]
        public bool? SafeUpdate { get; set; }

        /// <summary>
        /// An array of the numeric IDs of sharing agreements. Note that this replaces any existing agreements
        /// </summary>
        [JsonProperty("sharing_agreement_ids")]
        public IList<long> SharingAgreementIds { get; set; }

        /// <summary>
        /// The organization of the requester. You can only specify the ID of an organization associated with the requester. See <see href="https://developer.zendesk.com/rest_api/docs/core/organization_memberships">Organization Memberships</see>
        /// </summary>
        [JsonProperty("organization_id")]
        public long? OrganisationId { get; set; }

        [JsonProperty("brand_id")]
        public long? BrandId { get; set; }
        
        /// <summary>
        /// The id of the ticket form to use. Only applicable for enterprise accounts
        /// </summary>
        [JsonProperty("ticket_form_id")]
        public long? FormId { get; set; }
        
        /// <summary>
        /// Email cc to send notifications to on ticket creations or updates, based on trigger settings. See <see href="https://developer.zendesk.com/documentation/ticketing/managing-tickets/creating-and-updating-tickets/#setting-email-ccs">Email Ccs</see>.  
        /// </summary>
        [JsonProperty("email_ccs")]
        public IList<TicketEmailCc> EmailCcs { get; set; }
    }
}