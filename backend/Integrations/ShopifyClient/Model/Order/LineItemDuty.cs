﻿using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model.Order
{
    public class LineItemDuty : ShopifyObject
    {
        [JsonProperty("harmonized_system_code")] public string HarmonizedSystemCode { get; set; }

        [JsonProperty("country_code_of_origin")] public string CountryCodeOfOrigin { get; set; }

        [JsonProperty("shop_money")] public Price ShopMoney { get; set; }

        [JsonProperty("presentment_money")] public Price PresentmentMoney { get; set; }

        [JsonProperty("tax_lines")] public IEnumerable<TaxLine> TaxLines { get; set; }
    }
}