using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketFieldsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketFieldsResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTicketFields()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .TicketFields.GetAllAsync(new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
        }
    }
}