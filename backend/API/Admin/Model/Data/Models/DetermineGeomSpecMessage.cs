using Newtonsoft.Json;

namespace POD.API.Admin.Model.Data.Models
{
    public class DetermineGeomSpecMessage
    {
        [JsonProperty("fileId")]
        public int FileId { get; set; }
        
        [JsonProperty("containerName")]
        public string ContainerName { get; set; }
    }
}