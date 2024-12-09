using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class OutgoingFulfillmentRequestOptions
    {
        [JsonProperty("notify_customer")] public bool? NotifyCustomer { get; set; }
    }
}
