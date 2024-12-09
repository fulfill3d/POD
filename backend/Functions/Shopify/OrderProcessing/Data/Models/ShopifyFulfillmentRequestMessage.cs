﻿namespace POD.Functions.Shopify.OrderProcessing.Data.Models
{
    public class ShopifyFulfillmentRequestMessage
    {
        public string Shop { get; set; }
        public string Token { get; set; }
        public long ShopifyOrderId { get; set; }
    }
}