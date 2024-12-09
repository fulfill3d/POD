using Newtonsoft.Json;

namespace POD.API.Admin.Filament.Data.Models
{
    public class UpdateFilamentStockRequest
    {
        [JsonProperty("filamentId")]
        public decimal FilamentId { get; set; }
        
        [JsonProperty("cost")]
        public decimal Cost { get; set; }
        
        [JsonProperty("stockQuantity")]
        public int StockQuantity { get; set; }
    }
}