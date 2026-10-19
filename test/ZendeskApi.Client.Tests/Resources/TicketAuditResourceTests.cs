using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Resources;
using ZendeskApi.Client.Tests.ResourcesSampleSites;

namespace ZendeskApi.Client.Tests.Resources
{
    public class TicketAuditResourceTests : IDisposable
    {
        private readonly IZendeskApiClient _client;
        private readonly TicketAuditResource _resource;

        public TicketAuditResourceTests()
        {
            _client = new DisposableZendeskApiClient<TicketAudit>((resource) => new TicketAuditResourceSampleSite(resource));
            _resource = new TicketAuditResource(_client, NullLogger.Instance);
        }

        [Fact]
        [Obsolete]
        public async Task GetAllAsync_WhenCalled_ShouldGetAllTicketAudits()
        {
            var results = await _resource.GetAllAsync(cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(100, results.Audits.Count());
        }

        [Fact]
        [Obsolete]
        public async Task GetAllAsync_WhenCalledAndLimitSet_ShouldGetAllTicketAudits()
        {
            var results = await _resource.GetAllAsync(new CursorPagerVariant
            {
                ResultsLimit = 10
            }, cancellationToken: TestContext.Current.CancellationToken);

            Assert.Equal(10, results.Audits.Count());
        }

        [Fact]
        public async Task GetAllByTicketAsync_WhenCalled_ShouldGetAllTicketAudits()
        {
            var results = await _resource.GetAllByTicketAsync(1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results.Audits);
            Assert.Single(results.Audits);
        }

        [Fact]
        public async Task Get_WhenCalled_ShouldGetTicketAudits()
        {
            var results = await _resource.Get(1, 1, cancellationToken: TestContext.Current.CancellationToken);

            Assert.NotNull(results);
        }

        public void Dispose()
        {
            ((IDisposable)_client).Dispose();
        }
    }
}