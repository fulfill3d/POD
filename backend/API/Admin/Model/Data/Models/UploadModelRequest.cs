using Newtonsoft.Json;

namespace POD.API.Admin.Model.Data.Models
{
    public class UploadModelRequest
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("files")]
        public List<UploadModelFileRequest> Files { get; set; }
    }

    public class UploadModelFileRequest
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("content")]
        public byte[] Content { get; set; }
    }
}
