﻿using Newtonsoft.Json;

namespace POD.Integrations.ShopifyClient.Model
{
    /// <summary>
    /// An object representing a Shopify fulfillment request order line items.
    /// </summary>
    public class FulfillmentRequestOrderLineItem : ShopifyObject
    {
        /// <summary>
        /// The total number of units to be fulfilled.
        /// </summary>
        [JsonProperty("quantity")] public long? Quantity { get; set; }

    }
}
