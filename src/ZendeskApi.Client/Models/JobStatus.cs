using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;

namespace ZendeskApi.Client.Models
{
    public class JobStatusResponse
    {
        /// <summary>
        /// Automatically assigned when the job is queued
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// The URL to poll for status updates
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// The total number of tasks this job is batching through
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }

        /// <summary>
        /// Number of tasks that have already been completed
        /// </summary>
        [JsonPropertyName("progress")]
        public int Progress { get; set; }

        /// <summary>
        /// The current status. One of the following: "queued", "working", "failed", "completed", "killed"
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }

        /// <summary>
        /// Message from the job worker, if any
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        /// Result data from processed tasks
        /// </summary>
        [JsonPropertyName("results")]
        [JsonConverter(typeof(JobStatusResultConverter))]
        public IEnumerable<JobStatusResult> Results { get; set; }
    }

    [Description("JobStatus")]
    public class JobStatusResult
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("errors")]
        public string Errors { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
