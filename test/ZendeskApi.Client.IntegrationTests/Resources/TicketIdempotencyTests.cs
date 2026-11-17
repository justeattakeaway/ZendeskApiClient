using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Requests;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    /// <summary>
    /// Verifies Zendesk's Idempotency-Key behaviour on ticket creation, through the client:
    /// https://developer.zendesk.com/documentation/ticketing/managing-tickets/creating-and-updating-tickets/
    /// </summary>
    public class TicketIdempotencyTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        public TicketIdempotencyTests(ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private static TicketCreateRequest NewRequest(string body) => new TicketCreateRequest
        {
            Subject = "Idempotency integration test",
            Comment = new TicketComment { Body = body },
            Tags = new System.Collections.Generic.List<string> { "idempotency-integration-test" }
        };

        [Fact]
        public async Task CreateAsync_WhenSameKeyAndSameBodyRepeated_ShouldNotCreateAnotherTicket()
        {
            var client = _clientFactory.GetClient();
            var key = Guid.NewGuid().ToString();
            long? ticketId = null;

            try
            {
                var first = await client.Tickets.CreateAsync(NewRequest("same body"), key);
                ticketId = first.Ticket.Id;

                var second = await client.Tickets.CreateAsync(NewRequest("same body"), key);

                Assert.Equal(first.Ticket.Id, second.Ticket.Id);
            }
            finally
            {
                if (ticketId.HasValue)
                    await client.Tickets.DeleteAsync(ticketId.Value);
            }
        }

        [Fact]
        public async Task CreateAsync_WhenSameKeyAndDifferentBody_ShouldReturnBadRequestWithIdempotencyDescription()
        {
            var client = _clientFactory.GetClient();
            var key = Guid.NewGuid().ToString();
            long? ticketId = null;

            try
            {
                var first = await client.Tickets.CreateAsync(NewRequest("original body"), key);
                ticketId = first.Ticket.Id;

                var exception = await Assert.ThrowsAsync<ZendeskRequestException>(async () =>
                    await client.Tickets.CreateAsync(NewRequest("a different body"), key));

                Assert.Equal(HttpStatusCode.BadRequest, exception.Response.StatusCode);
                // OSAPI matches on this phrase to drop the message, so keep this in sync with CreateTicketCommandHandler
                Assert.Contains("match the given idempotency key", exception.Error.Description, StringComparison.OrdinalIgnoreCase);
            }
            finally
            {
                if (ticketId.HasValue)
                    await client.Tickets.DeleteAsync(ticketId.Value);
            }
        }

        [Fact]
        public async Task CreateAsync_WhenDifferentKeysAndSameBody_ShouldCreateSeparateTickets()
        {
            var client = _clientFactory.GetClient();
            long? firstId = null;
            long? secondId = null;

            try
            {
                var first = await client.Tickets.CreateAsync(NewRequest("same body"), Guid.NewGuid().ToString());
                firstId = first.Ticket.Id;
                var second = await client.Tickets.CreateAsync(NewRequest("same body"), Guid.NewGuid().ToString());
                secondId = second.Ticket.Id;

                Assert.NotEqual(first.Ticket.Id, second.Ticket.Id);
            }
            finally
            {
                if (firstId.HasValue) await client.Tickets.DeleteAsync(firstId.Value);
                if (secondId.HasValue) await client.Tickets.DeleteAsync(secondId.Value);
            }
        }
    }
}
