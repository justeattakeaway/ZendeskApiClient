using System.Reflection;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Converters;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Queries
{
    public static class ZendeskQueryTypeExtensions
    {
        public static IZendeskQuery WithTypeFilter<T>(this IZendeskQuery query) where T : ISearchResult
        {
            var id = typeof(T).GetTypeInfo().GetCustomAttribute<SearchResultTypeAttribute>().ResultType;
            return query.WithFilter("type", id);
        }

        public static IZendeskQuery WithWordFromTypeFilter<T>(this IZendeskQuery query, string value, Models.FilterOperator op = Models.FilterOperator.Equals)
        {
            var id = typeof(T).GetTypeInfo().GetCustomAttribute<SearchResultTypeAttribute>().ResultType;

            return query.WithFilter(id, value, op);
        }
    }
}