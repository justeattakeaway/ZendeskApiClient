using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class UserFieldsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public UserFieldsResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnUserFields()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .UserFields.GetAllAsync(new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
        }
    }
}