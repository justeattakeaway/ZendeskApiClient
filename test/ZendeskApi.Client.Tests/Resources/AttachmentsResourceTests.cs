using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class AttachmentsResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly AttachmentsResource _resource;

        public AttachmentsResourceTests()
        {
            _client = new DisposableZendeskApiClient<Attachment>((resource) => new AttachmentsResourceSampleSite(resource));
            _resource = new AttachmentsResource(_client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGet()
        {
            var attachment = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(1, attachment.Id);
            Assert.Equal("filename.1", attachment.FileName);
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
        public async Task UploadAsync_WhenCalled_ShouldCreateAttachment()
        {
            var byteArray = Encoding.UTF8.GetBytes("Hi there guys!");
            var stream = new MemoryStream(byteArray);

            var response = await _resource
                .UploadAsync("crash.log", stream, "6bk3gql82em5nmf", cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("6bk3gql82em5nmf", response.Token);
            Assert.Equal("text/plain", response.Attachment.ContentType);
            Assert.Equal(new MemoryStream(byteArray).Length, response.Attachment.Size);
        }

        [Fact]
        public async Task UploadAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.UploadAsync(string.Empty, new MemoryStream(), "6bk3gql82em5nmf", cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAsync_WhenCalled_ShouldDelete()
        {
#pragma warning disable CS0618
            await _resource.DeleteAsync("1", cancellationToken: TestContext.Current.CancellationToken);
#pragma warning restore CS0618
        }

        [Fact]
        public async Task DeleteAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
#pragma warning disable CS0618
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAsync(int.MinValue.ToString(), cancellationToken: TestContext.Current.CancellationToken));
#pragma warning restore CS0618
        }

        [Fact]
        public async Task DeleteUploadAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteUploadAsync("1", cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteUploadAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteUploadAsync(int.MinValue.ToString(), cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task DeleteAttachmentAsync_WhenCalled_ShouldDelete()
        {
            await _resource.DeleteAttachmentAsync(1, cancellationToken: TestContext.Current.CancellationToken);
        }

        [Fact]
        public async Task DeleteAttachmentAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.DeleteAttachmentAsync(int.MinValue, cancellationToken: TestContext.Current.CancellationToken));
        }

        [Fact]
        public async Task RedactAttachmentAsync_WhenCalled_ShouldReturnAttachment()
        {
            var attachment = await _resource.RedactAttachmentAsync(1, 2, 3, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(attachment);
            Assert.Equal(3, attachment.Id);
        }

        [Fact]
        public async Task RedactAttachmentAsync_WhenUnexpectedHttpCode_ShouldThrow()
        {
            await Assert.ThrowsAsync<ZendeskRequestException>(async () => await _resource.RedactAttachmentAsync(int.MinValue, 2, 3, cancellationToken: TestContext.Current.CancellationToken));
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}
