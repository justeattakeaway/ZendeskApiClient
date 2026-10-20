using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ZendeskApi.Client.Models
{
    public class Attachment : Thumbnail
    {
        [JsonPropertyName("thumbnails")]
        public List<Thumbnail> Thumbnails { get; set; }
    }
    
    public class Thumbnail
    {
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        [JsonPropertyName("file_name")]
        public string FileName { get; set; }

        [JsonPropertyName("content_url")]
        public string ContentUrl { get; set; }

        [JsonPropertyName("content_type")]
        public string ContentType { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }
    }
}
