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
    public class OrganizationFieldsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly OrganizationFieldsResource _resource;

        public OrganizationFieldsResourceTests()
        {
            _client = new DisposableZendeskApiClient<OrganizationField>((resource) => new OrganizationFieldsResourceSampleSite(resource));
            _resource = new OrganizationFieldsResource(_client, NullLogger.Instance);
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
                Assert.Equal($"raw.title.{i}", item.RawTitle);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new CursorPager{Size = 100}, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count());

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"raw.title.{i}", item.RawTitle);
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

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal("raw.title.2", item.RawTitle);
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
        public async Task GetAsync_WhenCalled_ShouldGetOrganizationField()
        {
            var item = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("raw.title.1", item.RawTitle);
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
        public async Task CreateAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.CreateAsync(new OrganizationField
            {
                Id = 101,
                RawTitle = "raw.title.101"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, item.Id);
            Assert.Equal("raw.title.101", item.RawTitle);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new OrganizationField
            {
                Id = int.MinValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }
        
        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdate()
        {
            var item = await _resource.UpdateAsync(new OrganizationField
            {
                Id = 1,
                RawTitle = "raw.title.1.new"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("raw.title.1.new", item.RawTitle);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new OrganizationField
            {
                Id = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new OrganizationField
            {
                Id = int.MinValue
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
    }
}