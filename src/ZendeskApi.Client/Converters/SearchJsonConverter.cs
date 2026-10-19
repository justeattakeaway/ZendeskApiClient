using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;
using ZendeskApi.Client.Responses;

namespace ZendeskApi.Client.Converters
{
    public class SearchJsonConverter : JsonConverter<SearchResponse<ISearchResult>>
    {
        private static readonly Type[] DeserializableSearchTypes =
        {
            typeof(Ticket),
            typeof(UserResponse),
            typeof(Group),
            typeof(Organization)
        };

        public override SearchResponse<ISearchResult> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var resultTypes = DeserializableSearchTypes.ToDictionary(
                k => k.GetTypeInfo().GetCustomAttribute<SearchResultTypeAttribute>().ResultType,
                v => v
            );

            using (var document = JsonDocument.ParseValue(ref reader))
            {
                var root = document.RootElement;

                var results = root.GetProperty("results")
                    .EnumerateArray()
                    .Select(result => (ISearchResult)result.Deserialize(resultTypes[result.GetProperty("result_type").GetString()], options))
                    .ToList();

                var nextPage = GetString(root, "next_page");
                var previousPage = GetString(root, "previous_page");

                return new SearchResponse<ISearchResult>
                {
                    Count = root.GetProperty("count").GetInt32(),
                    NextPage = nextPage != null ? new Uri(nextPage) : null,
                    PreviousPage = previousPage != null ? new Uri(previousPage) : null,
                    Results = results
                };
            }
        }

        public override void Write(Utf8JsonWriter writer, SearchResponse<ISearchResult> value, JsonSerializerOptions options)
        {
            throw new NotSupportedException();
        }

        private static string GetString(JsonElement element, string name)
        {
            return element.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String
                ? value.GetString()
                : null;
        }
    }
}
