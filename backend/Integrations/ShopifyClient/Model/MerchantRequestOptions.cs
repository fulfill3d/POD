using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class MerchantRequestOptions
    {
        [JsonProperty("shipping_method")] public string ShippingMethod { get; set; }

        [JsonProperty("note")] public string Note { get; set; }

        [JsonProperty("date")] public DateTimeOffset? Date { get; set; }
    }
}
