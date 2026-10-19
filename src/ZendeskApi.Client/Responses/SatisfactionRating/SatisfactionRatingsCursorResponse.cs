using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public class SatisfactionRatingsCursorResponse : CursorPaginationResponse<SatisfactionRating>
    {
        [JsonPropertyName("satisfaction_ratings")]
        public IEnumerable<SatisfactionRating> SatisfactionRatings { get; set; }

        protected override IEnumerable<SatisfactionRating> Enumerable => SatisfactionRatings;
    }
}
