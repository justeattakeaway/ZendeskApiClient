using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Models.AuditEvents;

namespace ZendeskApi.Client.Converters
{
    public class AuditEventJsonConverter : JsonConverter<IEnumerable<IAuditEvent>>
    {
        private readonly Dictionary<AuditTypes, Type> _typeMappings = new Dictionary<AuditTypes, Type>
        {
            {AuditTypes.Create, typeof(CreateEvent)},
            {AuditTypes.Change, typeof(ChangeEvent)},
            {AuditTypes.Comment, typeof(CommentEvent)},
            {AuditTypes.CommentRedactionEvent, typeof(CommentRedactionEvent)},
            {AuditTypes.AttachmentRedactionEvent, typeof(AttachmentRedactionEvent)},
            {AuditTypes.VoiceComment, typeof(VoiceCommentEvent)},
            {AuditTypes.CommentPrivacyChange, typeof(CommentPrivacyChangeEvent)},
            {AuditTypes.Notification, typeof(NotificationEvent)},
            {AuditTypes.NotificationWithCcs, typeof(NotificationWithCCsEvent)},
            {AuditTypes.Cc, typeof(CCEvent)},
            {AuditTypes.FollowerNotificationEvent, typeof(FollowerNotificationEvent)},
            {AuditTypes.FollowersChange, typeof(FollowerChangeEvent)},
            {AuditTypes.EmailCcChange, typeof(EmailCCChangeEvent)},
            {AuditTypes.SatisfactionRating, typeof(SatisfactionRatingEvent)},
            {AuditTypes.TicketSharingEvent, typeof(TicketSharingEvent)},
            {AuditTypes.OrganizationActivity, typeof(OrganizationActivityEvent)},
            {AuditTypes.Error, typeof(ErrorEvent)},
            {AuditTypes.Tweet, typeof(TweetEvent)},
            {AuditTypes.FacebookEvent, typeof(FacebookEvent)},
            {AuditTypes.FacebookComment, typeof(FacebookCommentEvent)},
            {AuditTypes.External, typeof(ExternalEvent)},
            {AuditTypes.LogMeInTranscript, typeof(LogMeInTranscriptEvent)},
            {AuditTypes.Push, typeof(PushEvent)}
        };
        
        public override IEnumerable<IAuditEvent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                reader.Skip();
                return null;
            }

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var result = new List<IAuditEvent>();

                foreach (var child in document.RootElement.EnumerateArray())
                {
                    if (child.ValueKind == JsonValueKind.Object &&
                        child.TryGetProperty("type", out var typeElement) &&
                        typeElement.ValueKind != JsonValueKind.Null &&
                        Enum.TryParse(typeElement.ValueKind == JsonValueKind.String ? typeElement.GetString() : typeElement.GetRawText(), out AuditTypes type))
                    {
                        result.Add((IAuditEvent)child.Deserialize(_typeMappings[type], options));
                    }
                }

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<IAuditEvent> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
