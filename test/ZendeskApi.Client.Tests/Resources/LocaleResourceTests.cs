using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Exceptions;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class LocaleResourceTests
    {
        private readonly LocaleResource _resource;

        public LocaleResourceTests()
        {
            IZendeskApiClient client = new DisposableZendeskApiClient<Locale>(resource => new LocaleResourceSampleSite(resource));
            _resource = new LocaleResource(client, NullLogger.Instance);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
            }
        }

        [Fact]
        public async Task GetAllPublicAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
            }
        }

        [Fact]
        public async Task GetAllAgentAsync_WhenCalled_ShouldGetAllUsers()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Count);

            for (var i = 1; i <= 100; i++)
            {
                var user = results.ElementAt(i - 1);

                Assert.Equal($"name.{i}", user.Name);
            }
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldGetUser()
        {
            var org = await _resource.GetAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("name.1", org.Name);
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
        public async Task GetCurrentAsync_WhenCalled_ShouldGetUser()
        {
            var org = await _resource.GetCurrentAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("name.1", org.Name);
        }

        [Fact]
        public async Task GetBestLanguageForUserAsync_WhenCalled_ShouldGetUser()
        {
            var org = await _resource.GetBestLanguageForUserAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal("name.1", org.Name);
        }
    }
}