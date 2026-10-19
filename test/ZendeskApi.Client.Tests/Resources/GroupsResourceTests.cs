using System;
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
{
    public class GroupsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly GroupsResource _resource;

        public GroupsResourceTests()
        {
            _client = new DisposableZendeskApiClient<Group>((resource) => new GroupsResourceSampleSite(resource));
            _resource = new GroupsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.ListAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.ListAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
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
        public async Task ListAsync_WhenCalledWithUserId_ShouldGetAll()
        {
            var results = await _resource.ListAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithUserIdWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.ListAsync(1, new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
        }

        [Fact]
        public async Task ListAsync_WithUserIdWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAsync(1, new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task ListAssignableAsync_WhenCalledWithUserId_ShouldGetAll()
        {
            var results = await _resource.ListAssignableAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task ListAssignableAsync_WhenCalledWithUserIdWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.ListAssignableAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
        }

        [Fact]
        public async Task ListAssignableAsync_WithUserIdWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.ListAssignableAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursor_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(
                new CursorPager {Size = 100}, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Groups.Count());

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
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
        public async Task GetAllByUserIdAsync_WhenCalledWithUserId_ShouldGetAll()
        {
            var results = await _resource.GetAllByUserIdAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WhenCalledWithUserIdWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByUserIdAsync(1, new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
        }

        [Fact]
        public async Task GetAllByUserIdAsync_WithUserIdWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByUserIdAsync(1, new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllByAssignableAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }


        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalledWithCursor_ShouldGetAll()
        {
            var results = await _resource.GetAllByAssignableAsync(
                new CursorPager {Size = 100}, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Groups.Count());

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"name.{i}", item.Name);
                Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{i}.json"), item.Url);
            }
        }

        [Fact]
        public async Task GetAllByAssignableAsync_WhenCalledWithUserIdWithOffsetPagination_ShouldGetAllOrganizations()
        {
            var results = await _resource.GetAllByAssignableAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal($"name.2", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/2.json"), item.Url);
        }

        [Fact] public async Task GetAllByAssignableAsync_WithUserIdWhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllByAssignableAsync(new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var item = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal($"name.1", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/1.json"), item.Url);
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
        public async Task CreateAsync_WhenCalled_ShouldCreateOrganization()
        {
            var item = await _resource.CreateAsync(new GroupCreateRequest("I'm a group!"), cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal($"I'm a group!", item.Name);
            Assert.Equal(new Uri($"https://company.zendesk.com/api/v2/groups/{item.Id}.json"), item.Url);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new GroupCreateRequest(""), cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.UpdateAsync(new GroupUpdateRequest(1)
            {
                Name = "name.1.new"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("name.1.new", item.Name);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new GroupUpdateRequest(int.MaxValue)
            {
                Name = "name.1.new"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new GroupUpdateRequest(int.MinValue)
            {
                Name = string.Empty
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDeleteOrganization()
        {
            await _resource.DeleteAsync(1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
