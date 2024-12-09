using Newtonsoft.Json;

namespace POD.API.Admin.Filament.Data.Models
{
    public class AddFilamentRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("color-id")]
        public int ColorId { get; set; }
        
        [JsonProperty("material-id")]
        public int MaterialId { get; set; }
        
        [JsonProperty("brand-id")]
        public int BrandId { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
        
        [JsonProperty("stock-quantity")]
        public int StockQuantity { get; set; }
        
        [JsonProperty("spool-weight")]
        public int SpoolWeight { get; set; }
        
        [JsonProperty("spool-weight-unit-id")]
        public int SpoolWeightUnitId { get; set; }
    }
}