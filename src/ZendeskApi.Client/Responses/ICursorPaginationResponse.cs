using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
        [JsonProperty("after_cursor")]
        public string AfterCursor { get; set; }
        [JsonProperty("before_cursor")]
        public string BeforeCursor { get; set; }
    }

    public class Links
    {
        [JsonProperty("prev")]
        public string Prev { get; set; }
        [JsonProperty("next")]
        public string Next { get; set; }
    }
}