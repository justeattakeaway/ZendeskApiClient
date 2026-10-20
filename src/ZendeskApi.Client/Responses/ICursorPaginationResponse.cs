using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public interface ICursorPagination<T> : IEnumerable<T>
    {
        Meta Meta { get; set; }
        Links Links { get; set; }
    }

    public abstract class CursorPaginationResponse<T> : ICursorPagination<T>
    {
        protected abstract IEnumerable<T> Enumerable { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Enumerable.GetEnumerator();
        }

        public Meta Meta { get; set; }
        public Links Links { get; set; }
    }

    public class Meta
    {
        [JsonPropertyName("has_more")]
        public bool HasMore { get; set; }
        [JsonPropertyName("after_cursor")]
        public string AfterCursor { get; set; }
        [JsonPropertyName("before_cursor")]
        public string BeforeCursor { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("prev")]
        public string Prev { get; set; }
        [JsonPropertyName("next")]
        public string Next { get; set; }
    }
}