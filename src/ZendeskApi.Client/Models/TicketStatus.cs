using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum TicketStatus
    {
        [EnumMember(Value = "new")]
        [JsonStringEnumMemberName("new")]
        New,
        [EnumMember(Value = "open")]
        [JsonStringEnumMemberName("open")]
        Open,
        [EnumMember(Value = "closed")]
        [JsonStringEnumMemberName("closed")]
        Closed,
        [EnumMember(Value = "pending")]
        [JsonStringEnumMemberName("pending")]
        Pending,
        [EnumMember(Value = "solved")]
        [JsonStringEnumMemberName("solved")]
        Solved,
        [EnumMember(Value = "hold")]
        [JsonStringEnumMemberName("hold")]
        Hold,
        [EnumMember(Value = "deleted")]
        [JsonStringEnumMemberName("deleted")]
        Deleted
    }
}