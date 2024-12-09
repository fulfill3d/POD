using Newtonsoft.Json;

namespace POD.API.Admin.Model.Data.Models
{
    public class UpdateModelRequest
    {
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
    }
    public class UpdateModelFileRequest
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
    }
}