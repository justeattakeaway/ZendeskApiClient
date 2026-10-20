using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status;

public enum Status
{
    [EnumMember(Value = "operational")]
    [JsonStringEnumMemberName("operational")]
    Operational,
    [EnumMember(Value = "impeded")]
    [JsonStringEnumMemberName("impeded")]
    Impeded,
    [EnumMember(Value = "inoperative")]
    [JsonStringEnumMemberName("inoperative")]
    Inoperative,
    [EnumMember(Value = "investigating")]
    [JsonStringEnumMemberName("investigating")]
    Investigating,
    [EnumMember(Value = "identified")]
    [JsonStringEnumMemberName("identified")]
    Identified
}