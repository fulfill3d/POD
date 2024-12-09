using Newtonsoft.Json;
using POD.Common.Core.Enum;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.DeleteProduct.Data.Models;
using POD.Functions.Shopify.DeleteProduct.Services.Interfaces;

namespace POD.Functions.Shopify.DeleteProduct.Services
{
    public class DeleteProductService(ICommonStoreProductService storeProductService)
        : IDeleteProductService
    {
        
        public async Task<bool> DeleteProducts(DeleteShopifyProductMessage request)
        {
            var productToDelete = JsonConvert.DeserializeObject<dynamic>(request.Content);
            long shopifyId = productToDelete.id;
            // The following line will be moved to POD.API.Seller.Store methods
            // We do not want to delete products from store here
            // Seller will set publish status to Idle
            // await _storeProductService.DeleteStoreProductByOnlineStoreId(id.ToString());

            var storeId = await storeProductService.GetStoreProductIdByOnlineStoreProductId(shopifyId.ToString());
            
            // TODO Clear OnlineStoreProductId and OnlineStoreProductVariantId columns

            await storeProductService.SetStoreProductPublishStatus(storeId, PublishStatus.Idle);
            
            return true;
        }
    }
}