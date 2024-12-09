using Microsoft.Extensions.Logging;
using POD.Common.Core.Enum;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.PublishProcessing.Data.Models;
using POD.Functions.Shopify.PublishProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.PublishProcessing.Services
{
    public class PublishProcessingService(
        ICommonStoreProductService commonStoreProductService,
        IProductService productService,
        IServiceBusService serviceBusService,
        ILogger<PublishProcessingService> logger) : IPublishProcessingService
    {

        public async Task StartPublishProcessingCallExecute(PublishProductMessage message)
        {
            await commonStoreProductService.SetStoreProductPublishStatus(message.StoreProductId, 
                PublishStatus.CreatingProductObject);
            
            var shopifyProductMessage = await productService.PrepareShopifyProductMessage(message);
            
            if (shopifyProductMessage == null) return;
            
            var callExecuteMessage = new ShopifyCallExecuteMessage<ShopifyProductMessage>
            {
                Data = shopifyProductMessage,
                MessageType = (int)POD.Common.Core.Enum.ShopifyCallExecuteMessageType.ShopifyCreateOrUpdateProduct
            };
            
            await commonStoreProductService.SetStoreProductPublishStatus(message.StoreProductId,
                PublishStatus.PublishingProduct);
            
            await serviceBusService.SendCallExecuteMessage(callExecuteMessage, 
                shopifyProductMessage.StoreId.ToString());
        }

        public async Task UpdatePublishProcessingCallExecute(ShopifyProductMessage message)
        {
            await commonStoreProductService.SetStoreProductPublishStatus(message.StoreProductId,
                PublishStatus.UpdatingProductImages);
            
            message = await productService.PrepareShopifyUpdateProductMessage(message);
            
            var callExecuteMessage = new ShopifyCallExecuteMessage<ShopifyProductMessage>
            {
                Data = message,
                MessageType = (int)POD.Common.Core.Enum.ShopifyCallExecuteMessageType.ShopifyUpdateProduct
            };
            
            await serviceBusService.SendCallExecuteMessage(callExecuteMessage, 
                message.StoreId.ToString());
        }

        public async Task PostPublishProcessingCallExecute(ShopifyProductMessage message)
        {
            await commonStoreProductService.SetStoreProductOnlineStoreIdById(
                message.StoreProductId,
                message.Product.Id.ToString());

            await SetOnlineStoreProductVariantIds(message);

            await commonStoreProductService.SetStoreProductPublishStatus(
                message.StoreProductId,
                PublishStatus.Published);
        }

        private async Task SetOnlineStoreProductVariantIds(ShopifyProductMessage message)
        {
            foreach (var variant in message.Product.Variants)
            {
                var variantId = await commonStoreProductService.GetStoreProductVariantIdBySku(variant.SKU);

                await commonStoreProductService.SetStoreProductVariantOnlineStoreProductVariantIdById(
                    variantId, variant.Id.ToString());
            }
        }
    }
}