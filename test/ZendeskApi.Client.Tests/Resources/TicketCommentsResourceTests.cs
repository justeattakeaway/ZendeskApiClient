using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{   public class TicketCommentsResourceTests
    {
        private readonly TicketCommentsResource _resource;
        private readonly TicketsResource _ticketResource;

        public TicketCommentsResourceTests()
        {
            var client = new DisposableZendeskApiClient<TicketResourceState, Ticket>(resource => new TicketResourceSampleSite(resource));
            _resource = new TicketCommentsResource(client, NullLogger.Instance);
            _ticketResource = new TicketsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAllCommentsForTicket()
        {
            var ticket = await _ticketResource.CreateAsync(new TicketCreateRequest("description") { Subject = "Test 1" }, cancellationToken: TestContext.Current.CancellationToken);

            var comments = await _resource.ListAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Single(comments);
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("description", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a comments..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.ListAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a second comment..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.ListAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(3, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);
            Assert.NotNull(comments.ElementAt(2).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(2).Body);
        }

        [Fact]
        public async Task GetAllAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAllCommentsForTicket()
        {
            var ticket = await _ticketResource.CreateAsync(new TicketCreateRequest("description") { Subject = "Test 1" }, cancellationToken: TestContext.Current.CancellationToken);

            var comments = await _resource.GetAllAsync(ticket.Ticket.Id, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);
            Assert.Single(comments);
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("description", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a comments..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.GetAllAsync(ticket.Ticket.Id, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a second comment..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.GetAllAsync(ticket.Ticket.Id, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(3, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);
            Assert.NotNull(comments.ElementAt(2).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(2).Body);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAllCommentsForTicket()
        {
            var ticket = await _ticketResource.CreateAsync(new TicketCreateRequest("description") { Subject = "Test 1" }, cancellationToken: TestContext.Current.CancellationToken);

            var comments = await _resource.GetAllAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Single(comments);
            Assert.NotNull(comments.ElementAt(0).Id);
            Assert.Equal("description", comments.ElementAt(0).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a comments..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.GetAllAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(2, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);

            await _resource.AddComment(ticket.Ticket.Id, new Models.TicketComment { Body = "Hi there! im a second comment..." }, cancellationToken: TestContext.Current.CancellationToken);

            comments = await _resource.GetAllAsync(ticket.Ticket.Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(3, comments.Count());
            Assert.NotNull(comments.ElementAt(1).Id);
            Assert.Equal("Hi there! im a comments...", comments.ElementAt(1).Body);
            Assert.NotNull(comments.ElementAt(2).Id);
            Assert.Equal("Hi there! im a second comment...", comments.ElementAt(2).Body);
        }

        [Fact]
        public async Task ListAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }
    }
}
