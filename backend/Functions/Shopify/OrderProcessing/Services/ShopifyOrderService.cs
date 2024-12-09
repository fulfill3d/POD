using POD.Functions.Shopify.OrderProcessing.Data.Models;
using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class ShopifyOrderService(
        IStoreOrderService storeOrderService,
        IStoreService storeService,
        IServiceBusService serviceBusService) : IShopifyOrderService
    {

        public async Task<bool> ProcessNewOrder(ShopifyNewOrderMessage message)
        {
            var shop = message.Shop;
            var order = message.Order;

            var store = await storeService.GetStoreByShopIdentifier(shop, order);

            await storeOrderService.SaveStoreOrder(store.Id, order);

            await serviceBusService.SendCreateFulfillmentRequestMessage(new ShopifyFulfillmentRequestMessage
            {
                Shop = store.ShopIdentifier,
                Token = store.Token,
                ShopifyOrderId = order.Id.GetValueOrDefault()
            }, store.Id);

            await SendUpdateInventoryMessagesForProducts(store, order); // TODO Not working

            return true;
        }

        private async Task SendUpdateInventoryMessagesForProducts(POD.Common.Database.Models.Store store, Order order)
        {
            var productIds = order.LineItems.Select(i => i.ProductId).Distinct();

            foreach (var shopifyProductId in productIds)
            {
                await serviceBusService.SendUpdateInventoryForProductMessage(
                    new ShopifyInventoryUpdateProduct
                    {
                        ProductOnlineStoreId = shopifyProductId.GetValueOrDefault(),
                        Shop = store.ShopIdentifier,
                        Token = store.Token,
                        StoreId = store.Id
                    });
            }
        }

    }
}
