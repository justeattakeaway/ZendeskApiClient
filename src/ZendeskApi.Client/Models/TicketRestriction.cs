using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum TicketRestriction
    {
        [EnumMember(Value = "organization")]
        [JsonStringEnumMemberName("organization")]
        Organization,
        [EnumMember(Value = "groups")]
        [JsonStringEnumMemberName("groups")]
        Groups,
        [EnumMember(Value = "assigned")]
        [JsonStringEnumMemberName("assigned")]
        Assigned,
        [EnumMember(Value = "requested")]
        [JsonStringEnumMemberName("requested")]
        Requested
    }
}