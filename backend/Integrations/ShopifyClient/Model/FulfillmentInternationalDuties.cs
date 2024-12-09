using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class FulfillmentInternationalDuties
    {
        /// <summary>
        /// The method of duties payment. Valid values:
        ///     DAP: Delivered at place.
        ///     DDP: Delivered duty paid.
        /// </summary>
        [JsonProperty("incoterm")] public string Incoterm { get; set; }

    }
}
