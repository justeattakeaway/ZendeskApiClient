using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public enum Impact
{
    [EnumMember(Value = "no impact")]
    [JsonStringEnumMemberName("no impact")]
    NoImpact,
    [EnumMember(Value = "minor")]
    [JsonStringEnumMemberName("minor")]
    Minor,
    [EnumMember(Value = "major")]
    [JsonStringEnumMemberName("major")]
    Major,
    [EnumMember(Value = "critical")]
    [JsonStringEnumMemberName("critical")]
    Critical,
    [EnumMember(Value = "security")]
    [JsonStringEnumMemberName("security")]
    Security
}