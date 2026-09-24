using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class UserIdentitiesResourceTests : IClassFixture<ZendeskClientFactory>, IDisposable
    {
        private readonly ZendeskClientFactory _clientFactory;
        private IZendeskClient _client;
        private const long TestUserId = 368420617118;
        private readonly List<long> CleanUpIdentityId = new List<long>();

        public UserIdentitiesResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _client = _clientFactory.GetClient();
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnUserIdentities()
        {
            var results = new UserIdentitiesCursorResponse();

            await _client.UserIdentities.CreateUserIdentityAsync(new UserIdentity()
            {
                Type = "twitter",
                Value = "JustEatTakeaway" // Value must match an existing twitter handle, otherwise ZD will throw
            }, TestUserId);

            results = (UserIdentitiesCursorResponse)await _client
                .UserIdentities.GetAllByUserIdAsync(TestUserId, new CursorPager());

            Assert.NotNull(results);

            var identityId = results.First(x => x.Type == "twitter").Id;
            Assert.NotNull(identityId);

            CleanUpIdentityId.Add(identityId.Value);
        }

        public void Dispose()
        {
            // Cannot delete user, as user has "opened tickets"
            // Cannot delete all Identities, as user "Must have at least one identity"
            // IdentityID must be provided, so we are stuck capturing IdentityIDs to be
            foreach (long identityId in CleanUpIdentityId)
            {
                Task.Run(async () => await _client.UserIdentities.DeleteAsync(TestUserId, identityId));
            }
        }
    }
}