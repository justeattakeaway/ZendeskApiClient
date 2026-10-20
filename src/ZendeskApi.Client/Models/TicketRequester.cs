using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    // partially documented under 
    // https://developer.zendesk.com/rest_api/docs/core/tickets#creating-a-ticket-with-a-new-requester
    [Description("Requester")]
    public class TicketRequester
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("locale_id")]
        public long? Locale { get; set; }
    }
}
