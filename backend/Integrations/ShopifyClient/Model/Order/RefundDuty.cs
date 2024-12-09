using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model.Order
{
    public class RefundDuty
    {
        [JsonProperty("duty_id")] public long? DutyId { get; set; }

        [JsonProperty("amount_set")] public PriceSet AmountSet { get; set; }
    }
}