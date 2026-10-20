using System;
using System.Collections.Generic;

using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    /// <summary>
    /// See: https://developer.zendesk.com/rest_api/docs/core/incremental_export#incremental-user-export
    /// 
    /// Pagination does not work like other pagination, with incremental
    /// export there will always be a next_page uri. The way to determine
    /// wether or not there are more resources is to see if the response
    /// contains a full list of a 1000 users. If less, then there is no
    /// newer results.
    /// </summary>
    public class IncrementalUsersResponse<T> : PaginationResponse<T> where T : UserResponse
    {

        [JsonConstructor]
        public IncrementalUsersResponse()
        {
        }

        /// <summary>
        /// For testing purposes
        /// </summary>
        public IncrementalUsersResponse(long endTime)
        {
            _endTime = endTime;
        }

        [JsonPropertyName("users")]
        public IEnumerable<T> Users { get; set; }

        protected override IEnumerable<T> Enumerable => Users;

        [JsonPropertyName("end_time")]
        private long _endTime { get; set; }

        [JsonIgnore]
        public DateTime EndTime => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(_endTime);

        [JsonIgnore]
        public bool HasMoreResults => Count == 1000;
    }
}
