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
    public class TicketFieldsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketFieldsResource _resource;

        public TicketFieldsResourceTests()
        {
            _client = new DisposableZendeskApiClient<TicketField>((resource) => new TicketFieldsResourceSampleSite(resource));
            _resource = new TicketFieldsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync();

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
            var results = await _resource.GetAllAsync(new CursorPager()
            {
                Size = 3
            });

            var item = results.ElementAt(1);

            Assert.Equal(2, item.Id);
            Assert.Equal("raw.title.2", item.RawTitle);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            });

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
            }));
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGetOrganization()
        {
            var item = await _resource.GetAsync(1);

            Assert.Equal(1, item.Id);
            Assert.Equal("raw.title.1", item.RawTitle);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.CreateAsync(new TicketField
            {
                Id = 101,
                RawTitle = "raw.title.101"
            });

            Assert.Equal(101, item.Id);
            Assert.Equal("raw.title.101", item.RawTitle);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new TicketField
            {
                Id = int.MinValue
            }));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdate()
        {
            var item = await _resource.UpdateAsync(new TicketField
            {
                Id = 1,
                RawTitle = "raw.title.1.new"
            });

            Assert.Equal(1, item.Id);
            Assert.Equal("raw.title.1.new", item.RawTitle);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new TicketField
            {
                Id = int.MaxValue
            });

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new TicketField
            {
                Id = int.MinValue
            }));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteAsync(1);
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue));
        }
    }
}
