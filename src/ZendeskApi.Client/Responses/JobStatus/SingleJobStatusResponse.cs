using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SingleJobStatusResponse
    {
        [JsonPropertyName("job_status")]
        public JobStatusResponse JobStatus { get; set; }
    }
}