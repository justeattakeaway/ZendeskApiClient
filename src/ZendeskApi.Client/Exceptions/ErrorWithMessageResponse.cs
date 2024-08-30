using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ZendeskApi.Client.Exceptions
{
    /// <summary />
    public class ErrorWithMessageResponse
    {
        /// <summary />
        public ErrorWithMessageResponse()
        {
        }

        /// <summary />
        [JsonProperty("error")]
        public ErrorWithMessage Error { get; internal set; }

        /// <summary />
        public ErrorResponse ToErrorResponse(JObject details = null) => new ErrorResponse()
        {
            Error       = Error?.Title,
            Description = Error?.Message,
            Details     = details
        };

        /// <summary />
        public class ErrorWithMessage()
        {
            /// <summary />
            [JsonProperty("message")]
            public string Message { get; internal set; }

            /// <summary />
            [JsonProperty("title")]
            public string Title { get; internal set; }
        }
    }
}
