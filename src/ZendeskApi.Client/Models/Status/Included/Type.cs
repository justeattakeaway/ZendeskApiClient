using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models.Status.Included;

public enum Type
{
    [EnumMember(Value = "service")]
    [JsonStringEnumMemberName("service")]
    Service,
    [EnumMember(Value = "incident_service")]
    [JsonStringEnumMemberName("incident_service")]
    IncidentService,
    [EnumMember(Value = "incident_update")]
    [JsonStringEnumMemberName("incident_update")]
    IncidentUpdate,
    [EnumMember(Value = "incident")]
    [JsonStringEnumMemberName("incident")]
    Incident
}