using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ZendeskApi.Client.Responses
{
    public interface ICursorPaginationVariantResponse<T> : IEnumerable<T>
    {
        [JsonProperty("after_cursor")]
        string AfterCursor { get; set; }
        [JsonProperty("before_cursor")]
        string BeforeCursor { get; set; }
    }

    public abstract class CursorPaginationVariantResponse<T> : ICursorPaginationVariantResponse<T>
    {
        [JsonProperty("after_cursor")]
        public string AfterCursor { get; set; }
        [JsonProperty("before_cursor")]
        public string BeforeCursor { get; set; }

        protected abstract IEnumerable<T> Enumerable { get; }

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