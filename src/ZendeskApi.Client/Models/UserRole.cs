using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public enum UserRole
    {
        [EnumMember(Value = "end-user")]
        [JsonStringEnumMemberName("end-user")]
        EndUser,
        [EnumMember(Value = "agent")]
        [JsonStringEnumMemberName("agent")]
        Agent,
        [EnumMember(Value = "admin")]
        [JsonStringEnumMemberName("admin")]
        Admin
    }
}