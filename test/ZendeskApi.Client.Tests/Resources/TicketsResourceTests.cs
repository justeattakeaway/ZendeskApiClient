using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using System.Collections.Generic;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Requests;
using ZendeskApi.Client.Responses;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketsResource _resource;

        public TicketsResourceTests()
        {
            _client = new DisposableZendeskApiClient<TicketResourceState, Ticket>((resource) => new TicketResourceSampleSite(resource));
            _resource = new TicketsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllAsync(new CursorPager()
            {
                Size = 3
            }, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.ElementAt(1);

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOffsetPagination_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllByOrganizationIdAsync(10, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(10, ticket.Id);
            Assert.Equal($"My printer is on fire! 10", ticket.Subject);
            Assert.Equal(10.ToString(), ticket.ExternalId);
            Assert.Equal(10, ticket.OrganisationId);
            Assert.Equal(10, ticket.RequesterId);
            Assert.Equal(10, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAllByOrganizationIdAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByOrganizationIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByRequestedByIdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllByRequestedByIdAsync(40, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(40, ticket.Id);
            Assert.Equal($"My printer is on fire! 40", ticket.Subject);
            Assert.Equal(40.ToString(), ticket.ExternalId);
            Assert.Equal(40, ticket.OrganisationId);
            Assert.Equal(40, ticket.RequesterId);
            Assert.Equal(40, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllByRequestedByIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAllByRequestedByIdAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAllByRequestedByIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByRequestedByIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByCcdIdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllByCcdIdAsync(20, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(20, ticket.Id);
            Assert.Equal($"My printer is on fire! 20", ticket.Subject);
            Assert.Equal(20.ToString(), ticket.ExternalId);
            Assert.Equal(20, ticket.OrganisationId);
            Assert.Equal(20, ticket.RequesterId);
            Assert.Equal(20, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllByCcdIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAllByCcdIdAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAllByCcdIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByCcdIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByAssignedToIdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllByAssignedToIdAsync(30, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(30, ticket.Id);
            Assert.Equal($"My printer is on fire! 30", ticket.Subject);
            Assert.Equal(30.ToString(), ticket.ExternalId);
            Assert.Equal(30, ticket.OrganisationId);
            Assert.Equal(30, ticket.RequesterId);
            Assert.Equal(30, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllByAssignedToIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAllByAssignedToIdAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAllByAssignedToIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByAssignedToIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByExternalIdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.GetAllByExternalIdAsync("40", cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(40, ticket.Id);
            Assert.Equal($"My printer is on fire! 40", ticket.Subject);
            Assert.Equal(40.ToString(), ticket.ExternalId);
            Assert.Equal(40, ticket.OrganisationId);
            Assert.Equal(40, ticket.RequesterId);
            Assert.Equal(40, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllByExternalIdAsync_WhenNotFound_ShouldReturnEmpty()
        {
            var results = await _resource.GetAllByExternalIdAsync("100000", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Empty(results);
        }

        [Fact]
        public async Task GetAllByExternalIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByExternalIdAsync(int.MinValue.ToString(), cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.ListAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOffsetPagination_ShouldGetAllTickets()
        {
            var results = await _resource.ListAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal("My printer is on fire! 2", ticket.Subject);
            Assert.Equal("2", ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListForOrganizationAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.ListForOrganizationAsync(10, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(10, ticket.Id);
            Assert.Equal($"My printer is on fire! 10", ticket.Subject);
            Assert.Equal(10.ToString(), ticket.ExternalId);
            Assert.Equal(10, ticket.OrganisationId);
            Assert.Equal(10, ticket.RequesterId);
            Assert.Equal(10, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListForOrganizationAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListForOrganizationAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListForOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListForOrganizationAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListRequestedByAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.ListRequestedByAsync(40, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(40, ticket.Id);
            Assert.Equal($"My printer is on fire! 40", ticket.Subject);
            Assert.Equal(40.ToString(), ticket.ExternalId);
            Assert.Equal(40, ticket.OrganisationId);
            Assert.Equal(40, ticket.RequesterId);
            Assert.Equal(40, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListRequestedByAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListRequestedByAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListRequestedByAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListRequestedByAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListCcdAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.ListCcdAsync(20, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(20, ticket.Id);
            Assert.Equal($"My printer is on fire! 20", ticket.Subject);
            Assert.Equal(20.ToString(), ticket.ExternalId);
            Assert.Equal(20, ticket.OrganisationId);
            Assert.Equal(20, ticket.RequesterId);
            Assert.Equal(20, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListCcdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListCcdAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListCcdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListCcdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListAssignedToAsync_WhenCalled_ShouldGetAllTickets()
        {
            var results = await _resource.ListAssignedToAsync(30, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(30, ticket.Id);
            Assert.Equal($"My printer is on fire! 30", ticket.Subject);
            Assert.Equal(30.ToString(), ticket.ExternalId);
            Assert.Equal(30, ticket.OrganisationId);
            Assert.Equal(30, ticket.RequesterId);
            Assert.Equal(30, ticket.AssigneeId);
        }

        [Fact]
        public async Task ListAssignedToAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.ListAssignedToAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task ListAssignedToAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAssignedToAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGetTicket()
        {
            var ticket = (await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken))
                .Ticket;

            Assert.Equal(1, ticket.Id);
            Assert.Equal($"My printer is on fire! 1", ticket.Subject);
            Assert.Equal(1.ToString(), ticket.ExternalId);
            Assert.Equal(1, ticket.OrganisationId);
            Assert.Equal(1, ticket.RequesterId);
            Assert.Equal(1, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithTicketIds_ShouldGetAllUsers()
        {
            var results = await _resource.GetAsync(new long[] { 1, 2, 3 }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithTicketIdsAndWithOffsetPagination_ShouldGetAllUsers()
        {
            var results = await _resource.GetAsync(
                new long[] { 1, 2, 3 },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal($"My printer is on fire! 2", ticket.Subject);
            Assert.Equal(2.ToString(), ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithTicketIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(new long[] { long.MinValue }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithTicketIds_ShouldGetAllUsers()
        {
            var results = await _resource.GetAllAsync(new long[] { 1, 2, 3 }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var ticket = results.ElementAt(i - 1);

                Assert.Equal(i, ticket.Id);
                Assert.Equal($"My printer is on fire! {i}", ticket.Subject);
                Assert.Equal(i.ToString(), ticket.ExternalId);
                Assert.Equal(i, ticket.OrganisationId);
                Assert.Equal(i, ticket.RequesterId);
                Assert.Equal(i, ticket.AssigneeId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithTicketIdsAndWithOffsetPagination_ShouldGetAllUsers()
        {
            var results = await _resource.GetAllAsync(
                new long[] { 1, 2, 3 },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            var ticket = results.First();

            Assert.Equal(2, ticket.Id);
            Assert.Equal($"My printer is on fire! 2", ticket.Subject);
            Assert.Equal(2.ToString(), ticket.ExternalId);
            Assert.Equal(2, ticket.OrganisationId);
            Assert.Equal(2, ticket.RequesterId);
            Assert.Equal(2, ticket.AssigneeId);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithTicketIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new long[] { long.MinValue }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreateTicket()
        {
            var response = await _resource.CreateAsync(
                new TicketCreateRequest("description")
                {
                    Subject = "My printer is on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful."
                    },
                    BrandId = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEqual(0L, response.Ticket.Id);
            Assert.Equal("My printer is on fire!", response.Ticket.Subject);
            Assert.Equal(1, response.Ticket.BrandId);
        }

        [Fact]
        public Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            return Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(
                new TicketCreateRequest("description")
                {
                    Subject = "My printer is no longer on fire!",
                    Comment = new TicketComment
                    {
                        Body = "The smoke is gone."
                    },
                    Tags = new List<string> { "error" }
                }, cancellationToken: TestContext.Current.CancellationToken));

            // could use tags to simulate httpstatus codes in fake client?
        }
        
        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdateTicket()
        {
            var ticket = (await CreateTickets(1)).First();
            
            Assert.Equal("My printer is on fire! 0", ticket.Subject); 
            Assert.Equal(1, ticket.BrandId);

            var updateTicketRequest = new TicketUpdateRequest(ticket.Id)
            {
                Subject = "I COMMAND YOU TO UPDATE!!!",
                BrandId = 2
            };

            ticket = (await _resource.UpdateAsync(updateTicketRequest, cancellationToken: TestContext.Current.CancellationToken)).Ticket;

            Assert.Equal("I COMMAND YOU TO UPDATE!!!", ticket.Subject);
            Assert.Equal(2, ticket.BrandId);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new TicketUpdateRequest(int.MaxValue)
            {
                Subject = "I COMMAND YOU TO UPDATE!!!"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new TicketUpdateRequest(int.MinValue)
            {
                Subject = string.Empty
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdateMultipleTickets()
        {
            var tickets = await CreateTickets(3);

            var updates = new List<TicketUpdateRequest>();
            var counter = 0;
            foreach (var ticket in tickets)
            {
                Assert.StartsWith("My printer is on fire! ", ticket.Subject);

                var updateTicketRequest = new TicketUpdateRequest(ticket.Id)
                {
                    Subject = "I COMMAND YOU TO UPDATE!!! " + counter++
                };

                updates.Add(updateTicketRequest);
            }

            var jobStatusResponse = await _resource.UpdateAsync(updates, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(jobStatusResponse);
        }

        [Fact]
        public async Task UpdateAsync_WhenManyAndUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () =>
            {
                await _resource.UpdateAsync(new [] { new TicketUpdateRequest(int.MinValue) }, cancellationToken: TestContext.Current.CancellationToken);
            });
        }

        [Fact]
        public async Task TagListsUpdateAsync_WhenCalled_ShouldUpdateTagsInMultipleTickets()
        {
            var tickets = await CreateTickets(3);

            var updateTicketRequest = new TicketTagListsUpdateRequest
            {
                AdditionalTags = new[] { "tag_1" },
                RemoveTags = new[] { "tag_to_remove" }
            };

            var jobStatusResponse = await _resource.TagListsUpdateAsync(tickets.Select(x => x.Id).ToArray(), updateTicketRequest, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(jobStatusResponse);

            var ticket1 = await _resource.GetAsync(tickets.First().Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Contains("tag_1", ticket1.Ticket.Tags);
            Assert.DoesNotContain("tag_to_remove", ticket1.Ticket.Tags);

            var ticket2 = await _resource.GetAsync(tickets.First().Id, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Contains("tag_1", ticket2.Ticket.Tags);
            Assert.DoesNotContain("tag_to_remove", ticket2.Ticket.Tags);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDeleteTicket()
        {
            var ticket = (await CreateTickets(1)).First();

            var ticket1 = await _resource.GetAsync(ticket.Id, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(JsonConvert.SerializeObject(ticket), JsonConvert.SerializeObject(ticket1.Ticket));

            await _resource.DeleteAsync(ticket.Id, cancellationToken: TestContext.Current.CancellationToken);

            var ticket2 = await _resource.GetAsync(ticket.Id, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(ticket2);
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDeleteMultiple()
        {
            var tickets = await CreateTickets(3);
            var ticketIds = tickets.Select(t => t.Id).ToArray();

            await _resource.DeleteAsync(ticketIds, cancellationToken: TestContext.Current.CancellationToken);

            var doTicketsStillExistResult = await _resource.GetAsync(ticketIds, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Empty(doTicketsStillExistResult);
        }

        private async Task<Ticket[]> CreateTickets(int numberOfTicketsToCreate)
        {
            var tickets = new List<TicketCreateRequest>();

            for (var i = 0; i < numberOfTicketsToCreate; i++)
            {
                var ticket = new TicketCreateRequest("Description is required")
                {
                    Subject = "My printer is on fire! " + i,
                    Comment = new TicketComment
                    {
                        Body = "The smoke is very colorful. " + i
                    },
                    BrandId = 1
                };

                tickets.Add(ticket);
            }

            return await CreateTickets(tickets.ToArray());
        }

        private async Task<Ticket[]> CreateTickets(params TicketCreateRequest[] tickets)
        {
            var createdTickets = new List<Ticket>();

            foreach (var ticketCreateRequest in tickets)
            {
                var response = await _resource.CreateAsync(ticketCreateRequest);
                createdTickets.Add(response.Ticket);
            }

            return createdTickets.ToArray();
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
