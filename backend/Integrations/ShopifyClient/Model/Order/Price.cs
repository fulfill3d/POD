using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model.Order
{
    public class Price
    {
        /// <summary>
        /// The three-letter code (ISO 4217 format) for currency.
        /// </summary>
        [JsonProperty("currency_code")] public string CurrencyCode { get; set; }

        /// <summary>
        /// The amount in the currency.
        /// </summary>
        [JsonProperty("amount")] public decimal? Amount { get; set; }
    }
}
