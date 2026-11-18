using Newtonsoft.Json;

namespace ZendeskApi.Client.Requests
{
    internal class TicketRequest<T>
    {
        public TicketRequest(T ticketCreateRequest)
        {
            Ticket = ticketCreateRequest;
        }

        [JsonProperty("ticket")]
        public T Ticket { get; set; }
    }
}