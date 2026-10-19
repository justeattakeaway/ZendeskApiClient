using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UsersRelatedInformationResourceTests
    {
        private readonly UsersResource _resource;

        public UsersRelatedInformationResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<UserRelatedInformationResponse>(resource => new UserRelatedInformationResourceSampleSite(resource));
            _resource = new UsersResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetRelatedInformationAsync_WhenCalled_ShouldGetUser()
        {
            var userRelatedInformation = await _resource.GetRelatedInformationAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, userRelatedInformation.AssignedTickets);
            Assert.Equal(2, userRelatedInformation.RequestedTickets);
            Assert.Equal(3, userRelatedInformation.CcdTickets);
            Assert.Equal(4, userRelatedInformation.OrganizationSubscriptions);
            Assert.Equal(5, userRelatedInformation.Topics);
            Assert.Equal(6, userRelatedInformation.TopicComments);
            Assert.Equal(7, userRelatedInformation.Votes);
            Assert.Equal(8, userRelatedInformation.Subscriptions);
            Assert.Equal(9, userRelatedInformation.EntrySubscriptions);
            Assert.Equal(10, userRelatedInformation.ForumSubscriptions);
        }

        [Fact]
        public async Task GetRelatedInformationAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetRelatedInformationAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetRelatedInformationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetRelatedInformationAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }
    }
}