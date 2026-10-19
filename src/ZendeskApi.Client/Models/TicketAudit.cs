using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Models.AuditEvents;

namespace ZendeskApi.Client.Models
{
    public class TicketAudit
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("ticket_id")]
        public long TicketId { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        
        [JsonPropertyName("author_id")]
        public long AuthorId { get; set; }
        
        [JsonPropertyName("events")]
        [JsonConverter(typeof(AuditEventJsonConverter))]
        public IEnumerable<IAuditEvent> Events { get; set; }

        [JsonIgnore]
        private Lazy<IEnumerable<CreateEvent>> LazyCreateEvents => new Lazy<IEnumerable<CreateEvent>>(() => Events.OfType<CreateEvent>());
        
        [JsonIgnore]
        public IEnumerable<CreateEvent> CreateEvents => LazyCreateEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<ChangeEvent>> LazyChangeEvents => new Lazy<IEnumerable<ChangeEvent>>(() => Events.OfType<ChangeEvent>());
        
        [JsonIgnore]
        public IEnumerable<ChangeEvent> ChangeEvents => LazyChangeEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<CommentEvent>> LazyCommentEvents => new Lazy<IEnumerable<CommentEvent>>(() => Events.OfType<CommentEvent>());

        [JsonIgnore]
        public IEnumerable<CommentEvent> CommentEvents => LazyCommentEvents.Value;
        
        [JsonIgnore]
        private Lazy<IEnumerable<CommentRedactionEvent>> LazyCommentRedactionEvents => new Lazy<IEnumerable<CommentRedactionEvent>>(() => Events.OfType<CommentRedactionEvent>());

        [JsonIgnore]
        public IEnumerable<CommentRedactionEvent> CommentRedactionEvents => LazyCommentRedactionEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<AttachmentRedactionEvent>> LazyAttachmentRedactionEvents => new Lazy<IEnumerable<AttachmentRedactionEvent>>(() => Events.OfType<AttachmentRedactionEvent>());

        [JsonIgnore]
        public IEnumerable<AttachmentRedactionEvent> AttachmentRedactionEvents => LazyAttachmentRedactionEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<VoiceCommentEvent>> LazyVoiceCommentEvents => new Lazy<IEnumerable<VoiceCommentEvent>>(() => Events.OfType<VoiceCommentEvent>());

        [JsonIgnore]
        public IEnumerable<VoiceCommentEvent> VoiceCommentEvents => LazyVoiceCommentEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<CommentPrivacyChangeEvent>> LazyCommentPrivacyChangeEvents => new Lazy<IEnumerable<CommentPrivacyChangeEvent>>(() => Events.OfType<CommentPrivacyChangeEvent>());

        [JsonIgnore]
        public IEnumerable<CommentPrivacyChangeEvent> CommentPrivacyChangeEvents => LazyCommentPrivacyChangeEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<NotificationEvent>> LazyNotificationEvents => new Lazy<IEnumerable<NotificationEvent>>(() => Events.OfType<NotificationEvent>());

        [JsonIgnore]
        public IEnumerable<NotificationEvent> NotificationEvents => LazyNotificationEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<NotificationWithCCsEvent>> LazyNotificationWithCCEvents => new Lazy<IEnumerable<NotificationWithCCsEvent>>(() => Events.OfType<NotificationWithCCsEvent>());

        [JsonIgnore]
        public IEnumerable<NotificationWithCCsEvent> NotificationWithCCsEvents => LazyNotificationWithCCEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<CCEvent>> LazyCCEvents => new Lazy<IEnumerable<CCEvent>>(() => Events.OfType<CCEvent>());

        [JsonIgnore]
        public IEnumerable<CCEvent> CcEvents => LazyCCEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<FollowerNotificationEvent>> LazyFollowerNotificationEvents => new Lazy<IEnumerable<FollowerNotificationEvent>>(() => Events.OfType<FollowerNotificationEvent>());

        [JsonIgnore]
        public IEnumerable<FollowerNotificationEvent> FollowerNotificationEvents => LazyFollowerNotificationEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<FollowerChangeEvent>> LazyFollowerChangeEvents => new Lazy<IEnumerable<FollowerChangeEvent>>(() => Events.OfType<FollowerChangeEvent>());

        [JsonIgnore]
        public IEnumerable<FollowerChangeEvent> FollowerChangeEvents => LazyFollowerChangeEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<EmailCCChangeEvent>> LazyEmailCcChangeEvents => new Lazy<IEnumerable<EmailCCChangeEvent>>(() => Events.OfType<EmailCCChangeEvent>());

        [JsonIgnore]
        public IEnumerable<EmailCCChangeEvent> EmailCcChangeEvents => LazyEmailCcChangeEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<SatisfactionRatingEvent>> LazySatisfactionRatingEvents => new Lazy<IEnumerable<SatisfactionRatingEvent>>(() => Events.OfType<SatisfactionRatingEvent>());

        [JsonIgnore]
        public IEnumerable<SatisfactionRatingEvent> SatisfactionRatingEvents => LazySatisfactionRatingEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<TicketSharingEvent>> LazyTicketSharingEvents => new Lazy<IEnumerable<TicketSharingEvent>>(() => Events.OfType<TicketSharingEvent>());
        [JsonIgnore]
        public IEnumerable<TicketSharingEvent> TicketSharingEvents => Events.OfType<TicketSharingEvent>();

        [JsonIgnore]
        private Lazy<IEnumerable<OrganizationActivityEvent>> LazyOrganizationActivityEvents => new Lazy<IEnumerable<OrganizationActivityEvent>>(() => Events.OfType<OrganizationActivityEvent>());

        [JsonIgnore]
        public IEnumerable<OrganizationActivityEvent> OrganizationActivityEvents => LazyOrganizationActivityEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<ErrorEvent>> LazyErrorEvents => new Lazy<IEnumerable<ErrorEvent>>(() => Events.OfType<ErrorEvent>());

        [JsonIgnore]
        public IEnumerable<ErrorEvent> ErrorEvents => LazyErrorEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<TweetEvent>> LazyTweetEvents => new Lazy<IEnumerable<TweetEvent>>(() => Events.OfType<TweetEvent>());

        [JsonIgnore]
        public IEnumerable<TweetEvent> TweetEvents => LazyTweetEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<FacebookEvent>> LazyFacebookEvents => new Lazy<IEnumerable<FacebookEvent>>(() => Events.OfType<FacebookEvent>());

        [JsonIgnore]
        public IEnumerable<FacebookEvent> FacebookEvents => LazyFacebookEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<FacebookCommentEvent>> LazyFacebookCommentEvents => new Lazy<IEnumerable<FacebookCommentEvent>>(() => Events.OfType<FacebookCommentEvent>());

        [JsonIgnore]
        public IEnumerable<FacebookCommentEvent> FacebookCommentEvents => LazyFacebookCommentEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<ExternalEvent>> LazyExternalEvents => new Lazy<IEnumerable<ExternalEvent>>(() => Events.OfType<ExternalEvent>());

        [JsonIgnore]
        public IEnumerable<ExternalEvent> ExternalEvents => LazyExternalEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<LogMeInTranscriptEvent>> LazyLogMeInTranscriptEvents => new Lazy<IEnumerable<LogMeInTranscriptEvent>>(() => Events.OfType<LogMeInTranscriptEvent>());

        [JsonIgnore]
        public IEnumerable<LogMeInTranscriptEvent> LogMeInTranscriptEvents => LazyLogMeInTranscriptEvents.Value;

        [JsonIgnore]
        private Lazy<IEnumerable<PushEvent>> LazyPushEvents => new Lazy<IEnumerable<PushEvent>>(() => Events.OfType<PushEvent>());

        [JsonIgnore]
        public IEnumerable<PushEvent> PushEvents => LazyPushEvents.Value;

    }
}