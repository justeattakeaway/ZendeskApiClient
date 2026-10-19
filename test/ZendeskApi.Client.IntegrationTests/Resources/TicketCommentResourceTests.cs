using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TicketCommentResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketCommentResourceTests(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnTicketComments()
        {
            var client = _clientFactory.GetClient();

            var ticket = await client.Tickets.CreateAsync(new TicketCreateRequest()
            {
                Comment = new TicketComment() {Body = "Printer is on fire"}
            }, cancellationToken: TestContext.Current.CancellationToken);

            var results = await client
                .TicketComments.GetAllAsync(ticket.Ticket.Id, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
            Assert.Single(results.Comments);
        }
    }
}