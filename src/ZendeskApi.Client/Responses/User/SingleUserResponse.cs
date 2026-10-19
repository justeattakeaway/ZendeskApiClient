using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Responses
{
    class SingleUserResponse
    {
        [JsonPropertyName("user")]
        public UserResponse UserResponse { get; set; }
    }
}
