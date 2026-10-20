using System;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class GroupsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public GroupsResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllAsync(new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();
            long? userId = null;
            long? groupId = null;

            string TestName(string entity) => $"{typeof(GroupsResourceTests).FullName}-{entity}-{DateTime.UtcNow}";

            try
            {
                var user = await client.Users.CreateAsync(new UserCreateRequest(TestName("user")), cancellationToken: TestContext.Current.CancellationToken);
                userId = user.Id;

                var group = await client.Groups.CreateAsync(new GroupCreateRequest(TestName("group")), cancellationToken: TestContext.Current.CancellationToken);
                groupId = group.Id;

                await client.Users.UpdateAsync(new UserUpdateRequest(userId.Value)
                {
                    DefaultGroupId = group.Id
                }, cancellationToken: TestContext.Current.CancellationToken);

                var results = await client
                    .Groups.GetAllByUserIdAsync(userId.Value, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

                Assert.NotNull(results);
            }
            finally
            {
                if (userId.HasValue)
                {
                    await client.Users.DeleteAsync(userId.Value, cancellationToken: TestContext.Current.CancellationToken);
                }
                if (groupId.HasValue)
                {
                    await client.Groups.DeleteAsync(groupId.Value, cancellationToken: TestContext.Current.CancellationToken);
                }
            }
        }

        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalledWithCursorPagination_ShouldReturnGroups()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .Groups.GetAllByAssignableAsync(new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
        }
    }
}