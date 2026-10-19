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
    public class RequestsResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly RequestsResource _resource;

        public RequestsResourceTests()
        {
            _client = new DisposableZendeskApiClient<Request>((resource) => new RequestsResourceSampleSite(resource));
            _resource = new RequestsResource(_client, NullLogger.Instance);
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
                Assert.Equal($"subject.{i}", item.Subject);
                Assert.Equal($"description.{i}", item.Description);
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
            Assert.Equal("subject.2", item.Subject);
            Assert.Equal("description.2", item.Description);
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
            Assert.Equal("subject.1", item.Subject);
            Assert.Equal("description.1", item.Description);
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
        public async Task GetAllComments_WhenCalled_ShouldGetAll()
        {
            var results = await _resource.GetAllComments(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var item = results.ElementAt(i - 1);

                Assert.Equal(i, item.Id);
                Assert.Equal($"body.{i}", item.Body);
            }
        }

        [Fact]
        public async Task GetAllComments_WhenCalledWithOffsetPagination_ShouldGetAll()
        {
            var results = await _resource.GetAllComments(1, new PagerParameters
            {
                Page = 2,
                PageSize = 1
            }, cancellationToken: TestContext.Current.CancellationToken);

            var item = results.First();

            Assert.Equal(2, item.Id);
            Assert.Equal("body.2", item.Body);
        }

        [Fact]
        public async Task GetAllComments_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetAllComments(1, new PagerParameters
            {
                Page = int.MaxValue,
                PageSize = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task GetTicketCommentAsync_WhenCalled_ShouldGetOrganization()
        {
            var item = await _resource.GetTicketCommentAsync(1, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("body.1", item.Body);
        }

        [Fact]
        public async Task GetTicketCommentAsync_WhenNotFound_ShouldReturnNull()
        {
            var results = await _resource.GetTicketCommentAsync(int.MaxValue, int.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(results);
        }

        [Fact]
        public async Task GetTicketCommentAsync_WhenServiceUnavailable_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.GetTicketCommentAsync(int.MinValue, int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreate()
        {
            var item = await _resource.CreateAsync(new Request
            {
                Id = 101,
                Subject = "subject.101",
                Description = "description.101"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(101, item.Id);
            Assert.Equal("subject.101", item.Subject);
            Assert.Equal("description.101", item.Description);
        }

        [Fact]
        public async Task CreateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.CreateAsync(new Request
            {
                Id = int.MinValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdate()
        {
            var item = await _resource.UpdateAsync(new Request
            {
                Id = 1,
                Subject = "subject.1.new",
                Description = "description.1.new"
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, item.Id);
            Assert.Equal("subject.1.new", item.Subject);
            Assert.Equal("description.1.new", item.Description);
        }

        [Fact]
        public async Task UpdateAsync_WhenNotFound_ShouldReturnNull()
        {
            var org = await _resource.UpdateAsync(new Request
            {
                Id = int.MaxValue
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(org);
        }

        [Fact]
        public async Task UpdateAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UpdateAsync(new Request
            {
                Id = int.MinValue
            }, cancellationToken: TestContext.Current.CancellationToken));
        }
    }
}