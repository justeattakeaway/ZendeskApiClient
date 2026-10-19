using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Text.Json.Serialization;
using ZendeskApi.Client.Models;

namespace ZendeskApi.Client.Responses
{
    public interface IPagination<T> : IEnumerable<T>
    {
        int Count { get; set; }
        Uri NextPage { get; set; }
        Uri PreviousPage { get; set; }
    }

    public abstract class PaginationResponse<T> : IPagination<T>
    {
        protected abstract IEnumerable<T> Enumerable { get; }

        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;

        [JsonPropertyName("next_page")]
        public Uri NextPage { get; set; } = null;

        [JsonPropertyName("previous_page")]
        public Uri PreviousPage { get; set; } = null;

        [JsonIgnore]
        public Pager Pager
        {
            get {
                if (NextPage == null)
                {
                    return new Pager(null, Count, 100);
                }

                var next = HttpUtility.ParseQueryString(NextPage.Query);
                var page = int.Parse(next["page"]);

                return new Pager(new PagerParameters { Page = page, PageSize = Count }, 100);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }
    }
}
