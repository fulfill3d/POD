using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class Webhook : ShopifyObject
    {
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)] public string CreatedAt { get; set; }
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)] public string UpdatedAt { get; set; }
        [JsonProperty("topic", NullValueHandling = NullValueHandling.Ignore)] public string Topic { get; set; }
        [JsonProperty("address", NullValueHandling = NullValueHandling.Ignore)] public string Address { get; set; }
        [JsonProperty("format", NullValueHandling = NullValueHandling.Ignore)] public string Format { get; set; }
    }
}
