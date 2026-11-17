using ZendeskApi.Client.Pagination;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.IntegrationTests.Factories
{
    public class CursorPaginatedIteratorFactory
    {
        private readonly ZendeskClientFactory zendeskClientFactory;
        public CursorPaginatedIteratorFactory(ZendeskClientFactory _zendeskClientFactory)
        {
            zendeskClientFactory = _zendeskClientFactory;
        }

        public CursorPaginatedIterator<T> Create<T>(ICursorPagination<T> response)
        {
            return new CursorPaginatedIterator<T>(response, zendeskClientFactory.GetApiClient());
        }
    }
}

