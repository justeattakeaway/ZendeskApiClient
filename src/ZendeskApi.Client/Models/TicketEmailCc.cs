using Newtonsoft.Json;

namespace ZendeskApi.Client.Models;

public class TicketEmailCc
{
    [JsonProperty("user_email")]
    public string UserEmail { get; set; }
    
    [JsonProperty("user_id")]
    public string UserId { get; set; }
    
    [JsonProperty("user_name")]
    public string Username { get; set; }
    
    [JsonProperty("action")]
    public TicketEmailCcAction Action { get; set; }
}