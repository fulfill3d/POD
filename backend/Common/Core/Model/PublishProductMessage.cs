using Newtonsoft.Json;

namespace POD.Common.Core.Model
{
    public class PublishProductMessage
    {
        [JsonProperty("sellerProductId")]
        public int SellerProductId { get; set; }

        [JsonProperty("marketPlaceId")]
        public int MarketPlaceId { get; set; }
    }
}