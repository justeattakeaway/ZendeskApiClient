using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class JobStatusResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory clientFactory;
        private readonly CursorPaginatedIteratorFactory cursorPaginatedIteratorFactory;
        public JobStatusResourceTests(
            ZendeskClientFactory _clientFactory)
        {
            clientFactory = _clientFactory;
            cursorPaginatedIteratorFactory = new CursorPaginatedIteratorFactory(clientFactory);
        }


        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = clientFactory.GetClient();

            var results = await client
                .JobStatuses.GetAllAsync(new CursorPager()
                {
                    Size = 2
                }, cancellationToken: TestContext.Current.CancellationToken);
            var iterator = cursorPaginatedIteratorFactory.Create<JobStatusResponse>(results);

            Assert.NotNull(results);
            Assert.Equal(2, iterator.Count());

            await iterator.NextPage();
            Assert.True(iterator.Count() <= 2);
        }

    }
}
