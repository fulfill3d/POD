namespace POD.Common.Core.Enum
{
    public enum ShopifyCallExecuteMessageType
    {
        /// <summary>
        /// The shopify create or update
        /// </summary>
        ShopifyCreateOrUpdateProduct = 1,

        /// <summary>
        /// The shopify update
        /// </summary>
        ShopifyUpdateProduct = 2,
        ShopifyCreateFulfillmentRequest = 3,
        ShopifyCreateFulfillment = 4,
        ShopifyGetLocationId = 5,
        ShopifyUpdateInventoryForProduct = 6
    }
}