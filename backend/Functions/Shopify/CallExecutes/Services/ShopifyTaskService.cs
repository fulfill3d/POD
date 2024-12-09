using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using POD.Common.Core.Enum;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyTaskService(
        IShopifyProductService shopifyProductService,
        ILogger<ShopifyTaskService> logger,
        IShopifyFulfillmentService shopifyFulfillmentService,
        IShopifyLocationService shopifyLocationService,
        IShopifyUpdateInventoryService shopifyUpdateInventoryService) : IShopifyTaskService
    {
        public async Task<bool> ProcessShopifyCallExecuteAsync(ShopifyCallExecuteMessage<JObject> message)
        {
            var messageType = (ShopifyCallExecuteMessageType)message.MessageType;

            switch (messageType)
            {
                case ShopifyCallExecuteMessageType.ShopifyCreateOrUpdateProduct:
                    return await shopifyProductService.ShopifyCreateOrUpdateProductAsync(message.Data.ToObject<ShopifyProductMessage>());
                case ShopifyCallExecuteMessageType.ShopifyUpdateProduct:
                    return await shopifyProductService.ShopifyUpdateProduct(message.Data.ToObject<ShopifyProductMessage>());
                case ShopifyCallExecuteMessageType.ShopifyCreateFulfillmentRequest:
                    return await shopifyFulfillmentService.CreateFulfillmentRequestAsync(message.Data.ToObject<ShopifyFulfillmentRequestMessage>());
                case ShopifyCallExecuteMessageType.ShopifyCreateFulfillment:
                    return await shopifyFulfillmentService.CreateFulfillmentAsync(message.Data.ToObject<ShopifyCreateFulfillmentMessage>());  
                case ShopifyCallExecuteMessageType.ShopifyGetLocationId:
                    return await shopifyLocationService.GetLocationIdForOrderFulfillment(message.Data.ToObject<ShopifyGetLocationIdForOrderFulfillmentMessage>());
                case ShopifyCallExecuteMessageType.ShopifyUpdateInventoryForProduct:
                    return await shopifyUpdateInventoryService.UpdateInventoryForProduct(message.Data.ToObject<UpdateInventoryForProductMessage>());
            }
            return false;
        }
    }
}
