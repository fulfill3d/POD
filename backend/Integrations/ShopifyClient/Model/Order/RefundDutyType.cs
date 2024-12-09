using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model.Order
{
    public class RefundDutyType
    {
        [JsonProperty("duty_id")] public long? DutyId { get; set; }

        [JsonProperty("refund_type")] public string RefundType { get; set; }
    }
}