using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterSectionsResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterSectionsResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync("en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync(new CursorPager(), "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnSections()
        {
            var client = _clientFactory.GetClient();

            var sections = await client
                .HelpCenter
                .Sections
                .GetAllAsync(360000599157, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(sections);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnSection()
        {
            var client = _clientFactory.GetClient();

            var section = await client
                .HelpCenter
                .Sections
                .GetAsync(360001138437, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(section);
        }
    }
}