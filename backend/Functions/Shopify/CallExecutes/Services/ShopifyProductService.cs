using Microsoft.Extensions.Logging;
using POD.Common.Core.Exception;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model.Product;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyProductService(
        IShopifyProductClientFactory shopifyProductClientFactory,
        IServiceBusService serviceBusService) : IShopifyProductService
    {
        public async Task<bool> ShopifyCreateOrUpdateProductAsync(ShopifyProductMessage result)
        {
            var shopifyProductClient = shopifyProductClientFactory.CreateClient(result.Store, result.Token);
            
            /* First Part */
            /*Shopify web service return exception when 2 call per seconds.*/
            /*Added thread sleep before service call (waiting 0,6 second).*/
            Thread.Sleep(600); // TODO App Config
            
            var product = result.Product;
            
            var shopifyPublishResponse = await shopifyProductClient.CreateOrUpdate(product);
            
            if (!shopifyPublishResponse.IsSuccessful || shopifyPublishResponse.Data == null)
            {
                throw new PodException(
                    $"CreateOrUpdate Product with customerProductId ({result.StoreProductId}) " +
                    $"failed update with images ,{shopifyPublishResponse.ErrorMessage} ");
            }
            
            var response = new ShopifyProductMessage
            {
                StoreProductId = result.StoreProductId,
                Product = shopifyPublishResponse.Data,
                StoreId = result.StoreId,
                Store = result.Store,
                Token = result.Token,
                IsProductExist = result.IsProductExist
            };
            
            await serviceBusService.SendUpdatePublishProcessingMessage(response);
            
            return true;
        }

        public async Task<bool> ShopifyUpdateProduct(ShopifyProductMessage shopifyProductMessage)
        {
            var shopifyProductClient = shopifyProductClientFactory.CreateClient(shopifyProductMessage.Store, shopifyProductMessage.Token);

            /*Shopify web service return exception when 2 call per seconds.*/
            /*Added thread sleep before service call (waiting 0,6 second).*/
            Thread.Sleep(1200);

            var alreadyUploadedImages = new List<ProductImage>();
            var productImages = new List<ProductImage>();
            productImages.AddRange(shopifyProductMessage.Product.Images);

            var batchSize = 15;

            while (productImages.Any())
            {
                //Add new batch to existing images
                alreadyUploadedImages.AddRange(productImages.Take(batchSize));
                productImages = productImages.Skip(batchSize).ToList();

                shopifyProductMessage.Product.Images = alreadyUploadedImages;

                var shopifyProduct = shopifyProductMessage.Product;
                shopifyProduct.Id = shopifyProductMessage.Product.Id;

                Thread.Sleep(1000);
                var publishedProductWithImagesResponse =
                    await shopifyProductClient.CreateOrUpdate(shopifyProduct);

                if (!publishedProductWithImagesResponse.IsSuccessful || publishedProductWithImagesResponse.Data == null)
                {
                    throw new PodException(
                        $"CreateOrUpdate Product with customerProductId " +
                        $"({shopifyProductMessage.StoreProductId}) failed update with images " +
                        $",{publishedProductWithImagesResponse.ErrorMessage})");
                }

                alreadyUploadedImages = publishedProductWithImagesResponse.Data.Images
                    .Where(i => i.Id.HasValue)
                    .ToList();
            }
            
            await serviceBusService.SendPostPublishProcessingMessage(shopifyProductMessage);
            
            await serviceBusService.SendUpdateInventoryForProductMessage(new ShopifyInventoryUpdateProduct
            {
                ProductOnlineStoreId = shopifyProductMessage.Product.Id.Value,
                StoreId = shopifyProductMessage.StoreId,
                Shop = shopifyProductMessage.Store,
                Token = shopifyProductMessage.Token,
            });
            
            return true;
        }
 
    }
}
