using POD.Common.Core.Enum;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using POD.Functions.Shopify.OrderProcessing.Data.Database;
using POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify;
using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class StoreOrderService(OrderProcessingContext dbContext, ICommonStoreProductService commonStoreProductService): IStoreOrderService
    {
        public async Task SaveStoreOrder(int storeId, Order order)
        {
            try
            {
                // TODO Verify All Order Items before proceeding
                var storeSaleOrderAddressId = await SaveStoreSaleOrderAddress(order.ShippingAddress);

                var storeSaleOrderId = await SaveStoreSaleOrder(storeId, storeSaleOrderAddressId, order);
                // TODO GENERATE ORDER NUMBER by STORE ORDER ID

                await SaveStoreSaleOrderDetails(storeSaleOrderId, order.LineItems);
                await SaveStoreSaleOrderStatus(storeSaleOrderId,
                    order.Test.GetValueOrDefault(false) ? POD.Common.Core.Enum.OrderStatus.TestOrder : POD.Common.Core.Enum.OrderStatus.Received);
            }
            catch (Exception e)
            {
                dbContext.RevertAllChangesInTheContext();
                throw;
            }
        }
        
        private StoreSaleOrderAddress PrepareStoreSaleOrderAddress(
            Data.Models.Shopify.Address address)
        {
            if (address == null) return null;

            var orderAddress = new POD.Common.Database.Models.StoreSaleOrderAddress();

            orderAddress.Address1 = address.Address1 ?? "";
            orderAddress.Address2 = address.Address2 ?? "";
            orderAddress.City = address.City ?? "";

            orderAddress.Country = address.CountryCode ?? "";
            if ("UN".Equals(orderAddress.Country.Trim(), StringComparison.OrdinalIgnoreCase))
                orderAddress.Country = "US";

            orderAddress.CountryCode = address.CountryCode ?? "";
            if ("UN".Equals(orderAddress.CountryCode.Trim(), StringComparison.OrdinalIgnoreCase))
                orderAddress.CountryCode = "US";

            orderAddress.FirstName = address.FirstName ?? "";
            orderAddress.LastName = address.LastName ?? "";
            orderAddress.Name = address.Name ?? "";

            if (address.Phone != null)
                orderAddress.Phone = address.Phone;
            else
                orderAddress.Phone = "";

            if (orderAddress.Phone.Length > 49)
                orderAddress.Phone = orderAddress.Phone.Substring(0, 49);

            orderAddress.Province = address.ProvinceCode ?? "";
            orderAddress.ProvinceCode = address.ProvinceCode ?? "";
            orderAddress.Zip = address.Zip ?? "";

            return orderAddress;
        }
        
        private async Task<int> SaveStoreSaleOrderAddress(POD.Functions.Shopify.OrderProcessing.Data.Models.Shopify.Address 
            address)
        {
            var storeOrderAddress = PrepareStoreSaleOrderAddress(address);
            await dbContext.StoreSaleOrderAddresses.AddAsync(storeOrderAddress);
            await dbContext.SaveChangesAsync();
            
            return storeOrderAddress.Id;
        }

        private StoreSaleOrder PrepareStoreSaleOrder(int storeId, 
            int storeSaleOrderAddressId, 
            Order order)
        {
            return new StoreSaleOrder
            {
                StoreId = storeId,
                StoreSaleOrderPriorityId = (int)OrderPriority.Normal,
                StoreSaleOrderAddressId = storeSaleOrderAddressId,
                ContactEmail = order.ContactEmail,
                StoreOrderIdentifier = order.Id.ToString(),
                StoreOrderNumber = order.OrderNumber.ToString(),
                OrderNumber = "ORDERNUMBERGENERATOR",
                IsEnabled = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        private async Task<int> SaveStoreSaleOrder(int storeId, int storeSaleOrderAddressId, Order order)
        {
            var storeSaleOrder = PrepareStoreSaleOrder(storeId, storeSaleOrderAddressId, order);
            await dbContext.StoreSaleOrders.AddAsync(storeSaleOrder);
            await dbContext.SaveChangesAsync();

            return storeSaleOrder.Id;
        }

        private StoreSaleOrderDetail PrepareStoreSaleOrderDetail(
            int storeSaleOrderId,
            int storeProductVariantId,
            LineItem item)
        {
            return new StoreSaleOrderDetail
            {
                StoreSaleOrderId = storeSaleOrderId,
                StoreProductVariantId = storeProductVariantId,
                OrderItemNumber = "GENERATE_ORDER_ITEM_NUMBER",

                Quantity = item.Quantity ?? 1,
                ItemPrice = 8,
                ShippingPrice = 1,
                Discount = 0,
                Total = 0,

                StorePrice = item.Price,
                StoreProductId = item.ProductId.ToString(),
                StoreOrderLineItemId = item.Id.ToString(),
                StoreVariantTitle = item.VariantTitle,

                IsEnabled = true,
                UpdatedAt = DateTime.UtcNow,
            };
            
        }

        private async Task SaveStoreSaleOrderDetails(int storeSaleOrderId, IEnumerable<LineItem> lineItems)
        {
            foreach (var lineItem in lineItems)
            {
                var storeProductVariantId = await commonStoreProductService.GetStoreProductVariantIdBySku(lineItem.SKU);
                var storeSaleOrderDetail = PrepareStoreSaleOrderDetail(storeSaleOrderId, storeProductVariantId, lineItem);
                await dbContext.StoreSaleOrderDetails.AddAsync(storeSaleOrderDetail);
            }

            await dbContext.SaveChangesAsync();
        }

        private StoreSaleOrderStatus PrepareStoreSaleOrderStatus(
            int storeSaleOrderId,
            POD.Common.Core.Enum.OrderStatus status)
        {
            return new StoreSaleOrderStatus
            {
                IsEnabled = true,
                UpdatedAt = DateTime.UtcNow,
                OrderStatusId = (int)status,
                StoreSaleOrderId = storeSaleOrderId,
            };
        }

        private async Task SaveStoreSaleOrderStatus(
            int storeSaleOrderId,
            POD.Common.Core.Enum.OrderStatus status)
        {
            var storeSaleOrderStatus = PrepareStoreSaleOrderStatus(storeSaleOrderId, status);
            
            await dbContext.StoreSaleOrderStatuses.AddAsync(storeSaleOrderStatus);
            
            await dbContext.SaveChangesAsync();
        }
    }
}