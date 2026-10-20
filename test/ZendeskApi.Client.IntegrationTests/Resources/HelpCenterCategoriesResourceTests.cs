using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

#pragma warning disable CS0618
namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterCategoriesResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterCategoriesResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnCategories()
        {
            var client = _clientFactory.GetClient();

            var categories = await client
                .HelpCenter
                .Categories
                .GetAllAsync("en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(categories);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursorPagination_ShouldReturnCategories()
        {
            var client = _clientFactory.GetClient();

            var categories = await client
                .HelpCenter
                .Categories
                .GetAllAsync(new CursorPager(), "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(categories);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnCategory()
        {
            var client = _clientFactory.GetClient();

            var category = await client
                .HelpCenter
                .Categories
                .GetAsync(360000599157, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(category);
        }
    }
}
