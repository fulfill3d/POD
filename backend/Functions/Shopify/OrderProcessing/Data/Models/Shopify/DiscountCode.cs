﻿using Newtonsoft.Json;

namespace POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify
{
    public class DiscountCode
    {
        /// <summary>
        /// The amount of the discount.
        /// </summary>
        [JsonProperty("amount")]
        public string Amount { get; set; }

        /// <summary>
        /// The discount code.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The type of discount. Known values are 'percentage', 'shipping', 'fixed_amount' and 'none'.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
