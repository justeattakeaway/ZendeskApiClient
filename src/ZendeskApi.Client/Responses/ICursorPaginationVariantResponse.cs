using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    public interface ICursorPaginationVariantResponse<T> : IEnumerable<T>
    {
        [JsonPropertyName("after_cursor")]
        string AfterCursor { get; set; }
        [JsonPropertyName("before_cursor")]
        string BeforeCursor { get; set; }
    }

    public abstract class CursorPaginationVariantResponse<T> : ICursorPaginationVariantResponse<T>
    {
        [JsonPropertyName("after_cursor")]
        public string AfterCursor { get; set; }
        [JsonPropertyName("before_cursor")]
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