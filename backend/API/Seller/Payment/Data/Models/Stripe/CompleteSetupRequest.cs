using Newtonsoft.Json;

namespace POD.API.Seller.Payment.Data.Models.Stripe
{
    public class CompleteSetupRequest
    {
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("payment_method")]
        public string PaymentMethodId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("usage")]
        public string Usage { get; set; }
    }
}
