using System.Linq;
using System.Text.Json;
using ZendeskApi.Client.Json;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Tests.Converters
{
    public class JobStatusResultConverterTests
    {
        [Fact]
        public void JobStatus_Result_AsObject_Deserializes_Into_Enumerable()
        {
            var json ="{\"job_status\":{\"id\":\"07d0f71323c2aba7707df154ef05b342\",\"url\":\"https://justeatukpoc1399564481.zendesk.com/api/v2/job_statuses/07d0f71323c2aba7707df154ef05b342.json\",\"total\":12,\"progress\":12,\"status\":\"completed\",\"message\":\"Completed at 2019-03-18 15:31:35 +0000\",\"results\":{\"success\":true}}}";

            var deserialized = JsonSerializer.Deserialize<SingleJobStatusResponse>(json, ZendeskJson.Options);
            Assert.Single(deserialized.JobStatus.Results);
            Assert.True(deserialized.JobStatus.Results.First().Success);
        }

        [Fact]
        public void JobStatus_Result_AsArray_WithOneItem_Deserializes_Into_Enumerable()
        {
            var json = "{\"job_status\":{\"id\":\"07d0f71323c2aba7707df154ef05b342\",\"url\":\"https://justeatukpoc1399564481.zendesk.com/api/v2/job_statuses/07d0f71323c2aba7707df154ef05b342.json\",\"total\":12,\"progress\":12,\"status\":\"completed\",\"message\":\"Completed at 2019-03-18 15:31:35 +0000\",\"results\":[{\"account_id\":523942,\"id\":354183,\"title\":\"Chatbot Chat\",\"status\":\"Deleted\",\"action\":\"delete\",\"success\":true,\"errors\":\"\"}]}}";
            
            var deserialized = JsonSerializer.Deserialize<SingleJobStatusResponse>(json, ZendeskJson.Options);
            Assert.Single(deserialized.JobStatus.Results);
        }

        [Fact]
        public void JobStatus_Result_AsArray_WithMultipleItems_Deserializes_Into_Enumerable()
        {
            var json = "{\"job_status\":{\"id\":\"07d0f71323c2aba7707df154ef05b342\",\"url\":\"https://justeatukpoc1399564481.zendesk.com/api/v2/job_statuses/07d0f71323c2aba7707df154ef05b342.json\",\"total\":12,\"progress\":12,\"status\":\"completed\",\"message\":\"Completed at 2019-03-18 15:31:35 +0000\",\"results\":[{\"account_id\":523942,\"id\":354183,\"title\":\"Chatbot Chat\",\"status\":\"Deleted\",\"action\":\"delete\",\"success\":true,\"errors\":\"\"},{\"account_id\":523942,\"id\":354182,\"title\":\"Chatbot Chat\",\"status\":\"Deleted\",\"action\":\"delete\",\"success\":true,\"errors\":\"\"}]}}";
            
            var deserialized = JsonSerializer.Deserialize<SingleJobStatusResponse>(json, ZendeskJson.Options);
            Assert.Equal(2, deserialized.JobStatus.Results.Count());
        }
        
    }
}