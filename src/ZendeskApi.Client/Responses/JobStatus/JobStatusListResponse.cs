using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class JobStatusListResponse : PaginationResponse<JobStatusResponse>
    {
        [JsonPropertyName("job_statuses")]
        public IEnumerable<JobStatusResponse> JobStatuses { get; set; }

        protected override IEnumerable<JobStatusResponse> Enumerable => JobStatuses;
    }
}