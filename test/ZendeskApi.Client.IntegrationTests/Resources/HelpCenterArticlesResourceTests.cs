using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class HelpCenterArticlesResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ITestOutputHelper _output;
        private readonly ZendeskClientFactory _clientFactory;

        public HelpCenterArticlesResourceTests(
            ITestOutputHelper output,
            ZendeskClientFactory clientFactory)
        {
            _output = output;
            _clientFactory = clientFactory;
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllAsync("en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCursor_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllAsync(new CursorPager{Size = 100}, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }


        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategory_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByCategoryIdAsync(360000599157, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithCategoryIdAndWithCursorPagination_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllByCategoryIdAsync(360000599157, new CursorPager(), "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithSection_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllBySectionIdAsync(360001138437, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalledWithSectionAndWithCursorPagination_ShouldReturnArticles()
        {
            var client = _clientFactory.GetClient();

            var articles = await client
                .HelpCenter
                .Articles
                .GetAllBySectionIdAsync(360001138437, new CursorPager(), "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotEmpty(articles);
        }

        [Fact]
        public async Task GetAsync_WhenCalled_ShouldReturnArticle()
        {
            var client = _clientFactory.GetClient();

            var article = await client
                .HelpCenter
                .Articles
                .GetAsync(360003979937, "en-gb", cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(article);
        }
    }
}