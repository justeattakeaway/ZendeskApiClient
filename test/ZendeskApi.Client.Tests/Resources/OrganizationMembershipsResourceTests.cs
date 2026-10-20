using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class OrganizationMembershipsResourceTests
    {
        private readonly OrganizationMembershipsResource _resource;

        public OrganizationMembershipsResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<OrganizationMembership>(resource => new OrganizationMembershipsResourceSampleSite(resource));
            _resource = new OrganizationMembershipsResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var membership = results.ElementAt(i - 1);

                Assert.Equal(i, membership.Id);
                Assert.Equal(i, membership.UserId);
                Assert.Equal(i, membership.OrganizationId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new CursorPager{Size = 100}, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count());

            for (var i = 1; i <= 100; i++)
            {
                var membership = results.ElementAt(i - 1);

                Assert.Equal(i, membership.Id);
                Assert.Equal(i, membership.UserId);
                Assert.Equal(i, membership.OrganizationId);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var membership = results.First();

            Assert.Equal(2, membership.Id);
            Assert.Equal(2, membership.UserId);
            Assert.Equal(2, membership.OrganizationId);
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
        public async Task GetAllForOrganizationAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllForOrganizationAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllForOrganizationAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllForOrganizationAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllForOrganizationAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllByOrganizationIdAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByOrganizationIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByOrganizationIdAsync(
                1,
                new CursorPager()
                {
                    Size = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Single(results);
        }

        [Fact]
        public async Task GetAllByOrganizationIdAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByOrganizationIdAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllForUserAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, new CursorPager(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.Single(results);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(
                1,
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(0, results.Count);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByUserIdAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var membership = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
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
        public async Task GetForUserAndOrganizationAsync_WhenCalled_ShouldGet()
        {
            var membership = await _resource.GetForUserAndOrganizationAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetForUserAndOrganizationAsync(int.MaxValue, int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetForUserAndOrganizationAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetForUserAndOrganizationAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetByUserIdAndOrganizationIdAsync_WhenCalled_ShouldGet()
        {
            var membership = await _resource.GetByUserIdAndOrganizationIdAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
        }

        [Fact]
        public async Task GetByUserIdAndOrganizationIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetByUserIdAndOrganizationIdAsync(int.MaxValue, int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetByUserIdAndOrganizationIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetByUserIdAndOrganizationIdAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreate()
        {
            var membership = await _resource.CreateAsync(new OrganizationMembership
            {
                Id = 101,
                UserId = 102,
                OrganizationId = 103
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, membership.Id);
            Assert.Equal(102, membership.UserId);
            Assert.Equal(103, membership.OrganizationId);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new OrganizationMembership
            {
               Id = int.MinValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task PostForUserAsync_WhenCalled_ShouldCreate()
        {
            var membership = await _resource.PostForUserAsync(new OrganizationMembership
            {
                Id = 101,
                UserId = 102,
                OrganizationId = 103
            }, 102, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, membership.Id);
            Assert.Equal(102, membership.UserId);
            Assert.Equal(103, membership.OrganizationId);
        }

        [Fact]
        public async Task PostForUserAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PostForUserAsync(new OrganizationMembership
            {
                Id = int.MinValue
            }, 102, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task PostByUserIdAsync_WhenCalled_ShouldCreate()
        {
            var membership = await _resource.PostByUserIdAsync(new OrganizationMembership
            {
                Id = 101,
                UserId = 102,
                OrganizationId = 103
            }, 102, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, membership.Id);
            Assert.Equal(102, membership.UserId);
            Assert.Equal(103, membership.OrganizationId);
        }

        [Fact]
        public async Task PostByUserIdAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.PostByUserIdAsync(new OrganizationMembership
            {
                Id = int.MinValue
            }, 102, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateAsync_WhenCalledWithMany_ShouldCreate()
        {
            var status = await _resource.CreateAsync(new List<OrganizationMembership>
            {
                new OrganizationMembership
                {
                    Id = 101,
                    UserId = 102,
                    OrganizationId = 103
                },
                new OrganizationMembership
                {
                    Id = 102,
                    UserId = 103,
                    OrganizationId = 104
                }
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(2, status.Total);
        }

        [Fact]
        public async Task CreateAsync_WithManyWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new List<OrganizationMembership>
            {
                new OrganizationMembership
                {
                    Id = int.MinValue
                }
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteAsync(1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalledWithUserId_ShouldDelete()
        {
            await _resource.DeleteAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteAsync_WithUserIdWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteByUserIdAsync_WhenCalledWithUserId_ShouldDelete()
        {
            await _resource.DeleteByUserIdAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteByUserIdAsync_WithUserIdWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteByUserIdAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDeleteMultiple()
        {
            await _resource.DeleteAsync(new long[] { 1, 2, 3 }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken));
            Assert.Null(await _resource.GetAsync(2, cancellationToken: TestContext.Current.CancellationToken));
            Assert.Null(await _resource.GetAsync(3, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WithManyWhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(new long[] { 1 }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task MakeDefault_WhenCalled_ShouldMakeDefault()
        {
            var results = await _resource.MakeDefault(1, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var membership = results.First();

            Assert.Equal(1, membership.Id);
            Assert.Equal(1, membership.UserId);
            Assert.Equal(1, membership.OrganizationId);
            Assert.True(membership.Default);
        }

        [Fact]
        public async Task MakeDefault_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.MakeDefault(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }
    }
}
