using System.Threading.Tasks;
using Xunit;
using ZendeskApi.Client.IntegrationTests.Factories;

namespace ZendeskApi.Client.IntegrationTests.Resources
{
    public class ServiceStatusResourceTests : IClassFixture<ZendeskClientFactory>
    {
        private readonly ZendeskClientFactory _clientFactory;

        private const string Subdomain = "d3v-just-eat";

        public ServiceStatusResourceTests(ZendeskClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(Subdomain)]
        public async Task ListActiveIncidents_WhenCalled_ShouldReturnIncidents(string subdomain)
        {
            var client = _clientFactory.GetClient();

            var activeIncidents = await client
                .ServiceStatus
                .ListActiveIncidents(subdomain, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(activeIncidents.Data);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(Subdomain)]
        public async Task ListMaintenanceIncidents_WhenCalled_ShouldReturnIncidents(string subdomain)
        {
            var client = _clientFactory.GetClient();

            var maintenanceIncidents = await client
                .ServiceStatus
                .ListMaintenanceIncidents(subdomain, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(maintenanceIncidents.Data);
        }
    }
}
