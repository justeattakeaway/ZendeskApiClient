using System.Runtime.Serialization;

namespace ZendeskApi.Client.Models;

public enum TicketEmailCcAction
{
    [EnumMember(Value = "put")]
    Put,
    [EnumMember(Value = "delete")]
    Delete,
}