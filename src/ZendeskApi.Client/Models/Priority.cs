using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum Priority
    {
        [EnumMember(Value = "urgent")]
        [JsonStringEnumMemberName("urgent")]
        Urgent,
        [EnumMember(Value = "high")]
        [JsonStringEnumMemberName("high")]
        High,
        [EnumMember(Value = "normal")]
        [JsonStringEnumMemberName("normal")]
        Normal,
        [EnumMember(Value = "low")]
        [JsonStringEnumMemberName("low")]
        Low
    }
}
