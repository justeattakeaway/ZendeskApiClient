using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;
#pragma warning disable CS0618 // Type or member is obsolete

namespace ZendeskApi.Client.Tests.Resources
{
    public class SatisfactionRatingsTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly SatisfactionRatingsResource _resource;

        public SatisfactionRatingsTests()
        {
            _client = new DisposableZendeskApiClient<State<SatisfactionRating>, SatisfactionRating>(resource => new SatisfactionRatingsResourceSampleSite(resource));
            _resource = new SatisfactionRatingsResource(_client, NullLogger.Instance);
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
                Assert.Equal($"comment.{i}", item.Comment);
            }
        }
        
        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldGetAll()
        {

            var results = await _resource.GetAllAsync(new CursorPager
            {
                Size = 5
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.ElementAt(1);

            Assert.Equal(2, item.Id);
            Assert.Equal("comment.2", item.Comment);
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
            Assert.Equal($"comment.2", item.Comment);
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
        public async Task GetAsync_WhenCalled_ShouldGetOrganization()
        {
            var item = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal($"comment.1", item.Comment);
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
            var item = await _resource.CreateAsync(new SatisfactionRating
            {
                Id = 101,
                Comment = "comment.101"
            }, 101, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, item.Id);
            Assert.Equal($"comment.101", item.Comment);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new SatisfactionRating
            {
                Id = int.MinValue
            }, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }
        
        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
