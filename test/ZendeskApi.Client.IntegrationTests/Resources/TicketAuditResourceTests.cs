using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketAuditResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketAuditResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorVariantPagination_ShouldReturnTicketAudits()
        {
            var client = _clientFactory.GetClient();

            var results = await client
                .TicketAudits.GetAllAsync(new CursorPagerVariant(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
            Assert.Equal(100, results.Count());
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();

            var cursorPager = new CursorPager { Size = 2 };
            var auditsPageOne = await client
                .TicketAudits.GetAllAsync(cursorPager, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(auditsPageOne);
            Assert.Equal(2, auditsPageOne.Count());
            Assert.True(auditsPageOne.Meta.HasMore);

            cursorPager.AfterCursor = auditsPageOne.Meta.AfterCursor;

            var auditsPageTwo = await client.TicketAudits.GetAllAsync(cursorPager, cancellationToken: TestContext.Current.CancellationToken);
            Assert.NotNull(auditsPageTwo);
            Assert.Equal(2, auditsPageTwo.Count());

            var auditIdsPageOne = auditsPageOne.Select(tag => tag.Id).ToList();
            var auditIdsPageTwo = auditsPageTwo.Select(tag => tag.Id).ToList();
            Assert.NotEqual(auditIdsPageOne, auditIdsPageTwo);
        }
    }
}