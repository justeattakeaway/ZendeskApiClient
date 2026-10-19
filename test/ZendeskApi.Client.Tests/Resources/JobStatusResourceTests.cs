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
    public class JobStatusResourceTests
    {
        private readonly JobStatusResource _resource;

        public JobStatusResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<JobStatusResponse>(resource => new JobStatusSampleSite(resource));
            _resource = new JobStatusResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task ListAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.ListAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i.ToString(), item.Id);
                Assert.Equal($"status.{i}", item.Status);
            }
        }

        [Fact]
        public async Task ListAsync_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.ListAsync(new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal("2", item.Id);
            Assert.Equal($"status.2", item.Status);
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
        public async Task GetAllAsync_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i.ToString(), item.Id);
                Assert.Equal($"status.{i}", item.Status);
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

            Assert.Equal("2", item.Id);
            Assert.Equal($"status.2", item.Status);
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
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var item = await _resource.GetAsync("1", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("1", item.Id);
            Assert.Equal("status.1", item.Status);
        }

        [Fact]
        public async Task GetAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetAsync(int.MaxValue.ToString(), cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(int.MinValue.ToString(), cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithIds_ShouldGetAll()
        {
            var results = await _resource.GetAsync(new string[] { "1", "2", "3" }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i.ToString(), item.Id);
                Assert.Equal($"status.{i}", item.Status);
            }
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithIdsAndWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAsync(
                new string[] { "1", "2", "3" },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2.ToString(), item.Id);
            Assert.Equal($"status.2", item.Status);
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAsync(new string[] { long.MinValue.ToString() }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithIds_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(new string[] { "1", "2", "3" }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(3, results.Count);

            for (var i = 1; i <= 3; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i.ToString(), item.Id);
                Assert.Equal($"status.{i}", item.Status);
            }
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithIdsAndWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllAsync(
                new string[] { "1", "2", "3" },
                new PagerParameters
                {
                    Page = 2,
                    PageSize = 1
                }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2.ToString(), item.Id);
            Assert.Equal($"status.2", item.Status);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithIdsButServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllAsync(new string[] { long.MinValue.ToString() }, cancellationToken: TestContext.Current.CancellationToken));
        }
    }
}
