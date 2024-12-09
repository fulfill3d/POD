using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    public class ShopifySeller : ShopifyObject
    {
        [JsonProperty("email")] public string Email { get; set; }

        [JsonProperty("address1")] public string Address1 { get; set; }

        [JsonProperty("zip")] public string Zip { get; set; }

        [JsonProperty("city")] public string City { get; set; }

        [JsonProperty("phone")] public string Phone { get; set; }

        [JsonProperty("address2")] public string Address2 { get; set; }

        [JsonProperty("country_code")] public string CountryCode { get; set; }

        [JsonProperty("shop_owner")] public string ShopOwner { get; set; }

        [JsonProperty("province_code")] public string ProvinceCode { get; set; }
    }
}
