using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class RequestsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;
        private readonly CursorPaginatedIteratorFactory cursorPaginatedIteratorFactory;

        public RequestsResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
            cursorPaginatedIteratorFactory = new CursorPaginatedIteratorFactory(clientFactory);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnRequests()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            var requests = await client
                .Requests
                .GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(requests);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnRequest()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            var created = await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.True(created.Id.HasValue);

            var request = await client
                .Requests
                .GetAsync(created.Id.Value, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(request);
        }

        [Fact]
        public async Task GetAllCommentsAsync_WhenCalled_ShouldReturnComments()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            var created = await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.True(created.Id.HasValue);

            var comments = await client
                .Requests
                .GetAllComments(created.Id.Value, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(comments);
            Assert.Contains(
                comments,
                comment => comment.Body.Contains($"ZendeskApi.Client.IntegrationTests {id}"));
        }

        [Fact]
        public async Task GetAllCommentsAsync_WhenCalledWithInvalidIds_ShouldReturnNull()
        {
            var client = _clientFactory.GetClient();

            var comments = await client
                .Requests
                .GetAllComments(long.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(comments);
        }

        [Fact]
        public async Task GetTicketCommentAsync_WhenCalled_ShouldReturnComments()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            var created = await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.True(created.Id.HasValue);

            var comments = await client
                .Requests
                .GetAllComments(created.Id.Value, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(comments);
            Assert.Contains(
                comments,
                comment => comment.Body.Contains($"ZendeskApi.Client.IntegrationTests {id}"));

            var createdComment = comments.First();

            Assert.True(createdComment.Id.HasValue);

            var commentByCommentId = await client
                .Requests
                .GetTicketCommentAsync(created.Id.Value, createdComment.Id.Value, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(commentByCommentId);
        }

        [Fact]
        public async Task GetTicketCommentAsync_WhenCalledWithInvalidIds_ShouldReturnNull()
        {
            var client = _clientFactory.GetClient();

            var comment = await client
                .Requests
                .GetTicketCommentAsync(long.MaxValue, long.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(comment);
        }

        [Fact]
        public async Task GetAsync_WhenCalledWithInvalidId_ShouldReturnNull()
        {
            var client = _clientFactory.GetClient();

            var request = await client
                .Requests
                .GetAsync(long.MaxValue, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Null(request);
        }

        [Fact]
        public async Task CreateAsync_WhenCalled_ShouldCreateRequest()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            var request = await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(request);
        }

        [Fact]
        public async Task UpdateAsync_WhenCalled_ShouldUpdateRequest()
        {
            var client = _clientFactory.GetClient();

            var id = Guid.NewGuid();

            var createdRequest = await client
                .Requests
                .CreateAsync(new Request
                {
                    Subject = $"ZendeskApi.Client.IntegrationTests {id}",
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {id}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.True(createdRequest.Id.HasValue);

            var updatedId = Guid.NewGuid();

            var updatedRequest = await client
                .Requests
                .UpdateAsync(new Request
                {
                    Id = createdRequest.Id.Value,
                    Comment = new TicketComment
                    {
                        Body = $"ZendeskApi.Client.IntegrationTests {updatedId}"
                    }
                }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(updatedRequest);

            var comments = await client
                .Requests
                .GetAllComments(createdRequest.Id.Value, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(comments);
            Assert.Contains(
                comments,
                comment => comment.Body.Contains($"ZendeskApi.Client.IntegrationTests {updatedId}"));
        }

        [Fact]
        public async Task GetAllAsync_And_GetAllComments_ShouldBePaginatable()
        {
            var client = _clientFactory.GetClient();
            var cursor = new CursorPager { Size = 1 };
            var requestsResponse = await client.Requests.GetAllAsync(cursor, cancellationToken: TestContext.Current.CancellationToken);

            var requestsIterator = cursorPaginatedIteratorFactory.Create<Request>(requestsResponse);
            Assert.Equal(1, requestsIterator.Count());

            var commentsResponse = await client.Requests.GetAllComments((long)requestsIterator.First().Id, cursor, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(1, commentsResponse.Count());

            await requestsIterator.NextPage();
            Assert.Equal(1, requestsIterator.Count());
            commentsResponse = await client.Requests.GetAllComments((long)requestsIterator.First().Id, cursor, cancellationToken: TestContext.Current.CancellationToken);
            Assert.Equal(1, commentsResponse.Count());
        }

    }
}
