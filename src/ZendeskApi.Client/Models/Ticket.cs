using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Models
{
    [SearchResultType("ticket")]
    public class Ticket : ISearchResult
    {
        /// <summary>
        /// Automatically assigned when creating tickets
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// The API url of this ticket
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// An id you can use to link Zendesk Support tickets to local records
        /// </summary>
        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        /// <summary>
        /// The type of this ticket, i.e. "problem", "incident", "question" or "task"
        /// </summary>
        [JsonPropertyName("type")]
        public TicketType? Type { get; set; }

        /// <summary>
        /// The value of the subject field for this ticket
        /// </summary>
        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// 	The dynamic content placeholder, if present, or the "subject" value, if not. See <see href="https://developer.zendesk.com/rest_api/docs/core/dynamic_content.html">Dynamic Content</see>
        /// </summary>
        [JsonPropertyName("raw_subject")]
        public string RawSubject { get; set; }

        /// <summary>
        /// The first comment on the ticket
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Priority, defines the urgency with which the ticket should be addressed: "urgent", "high", "normal", "low"
        /// </summary>
        [JsonPropertyName("priority")]
        public Priority? Priority { get; set; }

        /// <summary>
        /// The state of the ticket, "new", "open", "pending", "hold", "solved", "closed"
        /// </summary>
        [JsonPropertyName("status")]
        public TicketStatus Status { get; set; }

        /// <summary>
        /// The original recipient e-mail address of the ticket
        /// </summary>
        [JsonPropertyName("recipient")]
        public string Recipient { get; set; }

        /// <summary>
        /// The user who requested this ticket
        /// </summary>
        [JsonPropertyName("requester_id")]
        public long? RequesterId { get; set; }

        /// <summary>
        /// The user who submitted the ticket; The submitter always becomes the author of the first comment on the ticket.
        /// </summary>
        [JsonPropertyName("submitter_id")]
        public long? SubmitterId { get; set; }

        /// <summary>
        /// What agent is currently assigned to the ticket
        /// </summary>
        [JsonPropertyName("assignee_id")]
        public long? AssigneeId { get; set; }

        /// <summary>
        /// The organization of the requester. You can only specify the ID of an organization associated with the requester. See <see href="https://developer.zendesk.com/rest_api/docs/core/organization_memberships">Organization Memberships</see>
        /// </summary>
        [JsonPropertyName("organization_id")]
        public long? OrganisationId { get; set; }

        /// <summary>
        /// The group this ticket is assigned to
        /// </summary>
        [JsonPropertyName("group_id")]
        public long? GroupId { get; set; }

        /// <summary>
        /// Who are currently CC'ed on the ticket
        /// </summary>
        [JsonPropertyName("collaborator_ids")]
        public IReadOnlyList<long> CollaboratorIds { get; set; }

        /// <summary>
        /// The topic this ticket originated from, if any
        /// </summary>
        [JsonPropertyName("forum_topic_id")]
        public long? ForumTopicId { get; set; }

        /// <summary>
        /// The problem this incident is linked to, if any
        /// </summary>
        [JsonPropertyName("problem_id")]
        public long? ProblemId { get; set; }

        /// <summary>
        /// Is true of this ticket has been marked as a problem, false otherwise
        /// </summary>
        [JsonPropertyName("has_incidents")]
        public bool HasIncidents { get; set; }

        /// <summary>
        /// If this is a ticket of type "task" it has a due date. Due date format uses ISO 8601 format.
        /// </summary>
        [JsonPropertyName("due_at")]
        public DateTime? Due { get; set; }

        /// <summary>
        /// The array of tags applied to this ticket
        /// </summary>
        [JsonPropertyName("tags")]
        public IReadOnlyList<string> Tags { get; set; }

        /// <summary>
        /// This object explains how the ticket was created
        /// </summary>
        [JsonPropertyName("via")]
        public Via Via { get; set; }

        /// <summary>
        /// The custom fields of the ticket
        /// </summary>
        [JsonPropertyName("custom_fields")]
        [JsonConverter(typeof(CustomFieldsConverter))]
        public IReadOnlyCustomFields CustomFields { get; set; }

        /// <summary>
        /// The satisfaction rating of the ticket, if it exists, or the state of satisfaction, 'offered' or 'unoffered'
        /// </summary>
        [JsonPropertyName("satisfaction_rating")]
        public SatisfactionRating SatisfactionRating { get; set; }

        /// <summary>
        /// The ids of the sharing agreements used for this ticket
        /// </summary>
        [JsonPropertyName("sharing_agreement_ids")]
        public IReadOnlyList<long> SharingAgreementIds { get; set; }

        /// <summary>
        /// The ids of the followups created from this ticket - only applicable for closed tickets
        /// </summary>
        [JsonPropertyName("followup_ids")]
        public IReadOnlyList<long> FollowupIds { get; set; }

        /// <summary>
        /// The id of the ticket form to render for this ticket - only applicable for enterprise accounts
        /// </summary>
        [JsonPropertyName("ticket_form_id")]
        public long? FormId { get; set; }

        /// <summary>
        /// The id of the brand this ticket is associated with - only applicable for enterprise accounts
        /// </summary>
        [JsonPropertyName("brand_id")]
        public long? BrandId { get; set; }

        /// <summary>
        /// Is false if channelback is disabled, true otherwise - only applicable for channels framework ticket
        /// </summary>
        [JsonPropertyName("allow_channelback")]
        public bool AllowChannelback { get; set; }

        /// <summary>
        /// Is true if any comments are public, false otherwise
        /// </summary>
        [JsonPropertyName("is_public")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// When this record was created
        /// </summary>
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When this record last got updated
        /// </summary>
        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }


        [JsonPropertyName("result_type")]
        public string ResultType => "ticket";
    }
}
