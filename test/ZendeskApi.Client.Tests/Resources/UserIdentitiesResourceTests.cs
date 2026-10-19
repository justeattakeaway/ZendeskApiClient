using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable 618

namespace ZendeskApi.Client.Tests.Resources
{
    public class UserIdentitiesResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly UserIdentitiesResource _resource;

        public UserIdentitiesResourceTests()
        {
            _client = new DisposableZendeskApiClient<State<UserIdentity>, UserIdentity>((resource) => new UserIdentitiesResourceSampleSite(resource));
            _resource = new UserIdentitiesResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var identity = results.First();

            Assert.Equal(1, identity.Id);
            Assert.Equal(1, identity.UserId);
            Assert.Equal($"name.1", identity.Name);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllForUserAsync(1, new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Empty(results);
        }

        [Fact]
        public async Task GetAllForUserAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllForUserAsync(1, new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, results.Count);

            var identity = results.First();

            Assert.Equal(1, identity.Id);
            Assert.Equal(1, identity.UserId);
            Assert.Equal($"name.1", identity.Name);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, new CursorPager()
            {
                Size = 3
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Single(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Empty(results);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByUserIdAsync(1, new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetIdentityForUserAsync_WhenCalled_ShouldGet()
        {
            var item = await _resource.GetIdentityForUserAsync(10, 10, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(10, item.Id);
            Assert.Equal(10, item.UserId);
            Assert.Equal($"name.10", item.Name);
        }

        [Fact]
        public async Task GetIdentityForUserAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetIdentityForUserAsync(int.MaxValue, int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetIdentityForUserAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetIdentityForUserAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetIdentityByUserIdAsync_WhenCalled_ShouldGet()
        {
            var item = await _resource.GetIdentityByUserIdAsync(10, 10, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(10, item.Id);
            Assert.Equal(10, item.UserId);
            Assert.Equal($"name.10", item.Name);
        }

        [Fact]
        public async Task GetIdentityByUserIdAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetIdentityByUserIdAsync(int.MaxValue, int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetIdentityByUserIdAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetIdentityByUserIdAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateUserIdentityAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                Id = 101,
                Name = "name.101",
                UserId = 101
            }, 101, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, item.Id);
            Assert.Equal("name.101", item.Name);
        }

        [Fact]
        public async Task CreateUserIdentityAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateUserIdentityAsync(new UserIdentity
            {
                Id = int.MinValue,
                UserId = int.MinValue
            }, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateEndUserIdentityAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.CreateEndUserIdentityAsync(new UserIdentity
            {
                Id = 102,
                Name = "name.102",
                UserId = 102
            }, 102, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(102, item.Id);
            Assert.Equal("name.102", item.Name);
        }

        [Fact]
        public async Task CreateEndUserIdentityAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateEndUserIdentityAsync(new UserIdentity
            {
                Id = int.MinValue,
                UserId = int.MinValue
            }, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdate()
        {
            var item = await _resource.UpdateAsync(new UserIdentity
            {
                Id = 1,
                Name = "name.new.1",
                UserId = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("name.new.1", item.Name);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new UserIdentity
            {
                Id = int.MaxValue,
                UserId = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new UserIdentity
            {
                Id = int.MinValue,
                UserId = int.MinValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}