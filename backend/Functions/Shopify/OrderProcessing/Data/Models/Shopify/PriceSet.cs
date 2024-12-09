using Newtonsoft.Json;

namespace POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify
{
    public class PriceSet
    {
        [JsonProperty("shop_money")]
        public Price ShopMoney { get; set; }

        [JsonProperty("presentment_money")]
        public Price PresentmentMoney { get; set; }
    }
}