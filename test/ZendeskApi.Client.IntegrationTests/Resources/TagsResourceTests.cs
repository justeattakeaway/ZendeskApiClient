using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class TagsResourceTest : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TagsResourceTest(
            ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();
            Ticket ticket = await CreateTicketWithTagsAsync(client);

            try
            {
                var cursorPager = new CursorPager { Size = 2 };
                var tagsPageOne = await client
                    .Tags.GetAllAsync(cursorPager, cancellationToken: TestContext.Current.CancellationToken);

                Assert.NotNull(tagsPageOne);
                Assert.Equal(2, tagsPageOne.Count());
                Assert.True(tagsPageOne.Meta.HasMore);

                cursorPager.AfterCursor = tagsPageOne.Meta.AfterCursor;

                var tagsPageTwo = await client.Tags.GetAllAsync(cursorPager, cancellationToken: TestContext.Current.CancellationToken);
                Assert.NotNull(tagsPageTwo);
                Assert.Equal(2, tagsPageTwo.Count());

                var tagIdsPageOne = tagsPageOne.Select(tag => tag.Name).ToList();
                var tagIdsPageTwo = tagsPageTwo.Select(tag => tag.Name).ToList();
                Assert.NotEqual(tagIdsPageOne, tagIdsPageTwo);
            }
            finally
            {
                await CleanupTicketAsync(client, ticket);
            }

        }

        private async Task<Ticket> CreateTicketWithTagsAsync(IZendeskClient client)
        {
            var ticketResponse = await client.Tickets.CreateAsync(new Requests.TicketCreateRequest {
                Tags = new List<string> { "apac", "shipping", "sales", "important", "sla" },
                Comment = new TicketComment { Body = "This is a ticket with 5 of tags" },
                Subject = "Test ticket"
                }
            );
            return ticketResponse.Ticket;
        }

        private async Task CleanupTicketAsync(IZendeskClient client, Ticket ticket)
        {
            await client.Tickets.DeleteAsync(ticket.Id); 
        }
    }
}