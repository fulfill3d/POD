using Microsoft.Extensions.Logging;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyFulfillmentService(
        IShopifyFulfillmentRequestClientFactory shopifyFulfillmentRequestClientFactory,
        ILogger<ShopifyFulfillmentService> logger,
        IShopifyLocationService shopifyLocationService,
        IShopifyFulfillmentOrderService shopifyFulfillmentOrderService,
        IShopifyOrderFulfillmentClientFactory shopifyOrderFulfillmentClientFactory,
        IServiceBusService serviceBusService) : IShopifyFulfillmentService
    {
        public async Task<bool> CreateFulfillmentRequestAsync(ShopifyFulfillmentRequestMessage shopifyFulfillmentRequestMessage)
        {

            var podLocationAsync = 
                await shopifyLocationService
                    .GetPodLocationAsync(
                        shopifyFulfillmentRequestMessage.Shop, 
                        shopifyFulfillmentRequestMessage.Token);

            if (podLocationAsync == null) return false;

            var podFulfillmentOrders =
                await shopifyFulfillmentOrderService.GetPodFulfillmentOrdersAsync(
                    shopifyFulfillmentRequestMessage.Shop,
                    shopifyFulfillmentRequestMessage.Token,
                    shopifyFulfillmentRequestMessage.ShopifyOrderId,
                    podLocationAsync.Id.GetValueOrDefault());

            if( podFulfillmentOrders == null || !podFulfillmentOrders.Any())
            {
                logger.LogError("Cannot find pod fulfillment order");
                return false;
            }

            var shopifyFulfillmentRequestClient = 
                shopifyFulfillmentRequestClientFactory.CreateClient(
                    shopifyFulfillmentRequestMessage.Shop, 
                    shopifyFulfillmentRequestMessage.Token);

            foreach (var fulfillmentOrder in podFulfillmentOrders)
            {
                _ = await SubmitAndAcceptFulfillmentRequest(
                        fulfillmentOrder.Id, 
                        fulfillmentOrder.RequestStatus, 
                        shopifyFulfillmentRequestClient);
            }

            return true;
        }

        private async Task<bool> AcceptFulfillmentRequest(
            long? fulfillmentOrderId,
            IShopifyFulfillmentRequestClient shopifyFulfillmentRequestClient)
        {
            var acceptResponse = await shopifyFulfillmentRequestClient.AcceptAsync(fulfillmentOrderId.GetValueOrDefault());

            if (!acceptResponse.IsSuccessful)
            {
                logger.LogError("Cannot accept fulfillment request for fulfillment order {fulfillmentOrderId}", fulfillmentOrderId);
                return false;
            }

            return true;
        }

        private async Task<bool> CreateFulfillmentRequestAsync(
            long? fulfillmentOrderId,
            string fulfillmentOrderRequestStatus,
            IShopifyFulfillmentRequestClient fulfillmentRequestClient)
        {
            //Fulfillment Request is already submitted. We can directly accept it.
            if (fulfillmentOrderRequestStatus.Equals(
                    FulfillmentOrderRequestStatuses.SUBMITTED, 
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            //Fulfillment Request either cancelled or already accepted. We can ignore it.
            if(!fulfillmentOrderRequestStatus.Equals(
                   FulfillmentOrderRequestStatuses.UNSUBMITTED,
                    StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var createResponse = await fulfillmentRequestClient.CreateAsync(
                fulfillmentOrderId.GetValueOrDefault(),
                new FulfillmentRequest());

            if (!createResponse.IsSuccessful)
            {
                logger.LogError("Cannot request fulfillment for fulfillment order {fulfillmentOrderId}", fulfillmentOrderId);
                return false;
            }
            
            return true;
        }

        public async Task<bool> CreateFulfillmentAsync(ShopifyCreateFulfillmentMessage shopifyCreateFulfillmentMessage)
        {
            var podFulfillmentOrders =
                await shopifyFulfillmentOrderService.GetPodFulfillmentOrdersAsync(
                    shopifyCreateFulfillmentMessage.Shop,
                    shopifyCreateFulfillmentMessage.Token,
                    shopifyCreateFulfillmentMessage.ShopifyOrderId,
                    shopifyCreateFulfillmentMessage.LocationId);

            if (podFulfillmentOrders == null || !podFulfillmentOrders.Any())
            {
                logger.LogError("Cannot find pod fulfillment order");
                return false;
            }

            var shopifyOrderFulfillmentClient = 
                shopifyOrderFulfillmentClientFactory.CreateClient(
                    shopifyCreateFulfillmentMessage.Shop,
                    shopifyCreateFulfillmentMessage.Token);

            var isSuccess = true;

            var lineItemList = shopifyCreateFulfillmentMessage.ShopifyOrderLineItems.ToList();

            var shopifyFulfillmentRequestClient =
                shopifyFulfillmentRequestClientFactory.CreateClient(
                    shopifyCreateFulfillmentMessage.Shop,
                    shopifyCreateFulfillmentMessage.Token);

            foreach (var order in podFulfillmentOrders)
            {
                var shippedLineItems = 
                    GetShippedLineItems(
                        order.FulfillmentOrderLineItems, 
                        lineItemList);

                if (shippedLineItems == null || !shippedLineItems.Any()) continue;

                //Fulfillment request should have been accepted when the order is created
                //but somehow some orders are stuck in submitted state.
                //That is why we are trying to accept it again.
                var isFulfillmentRequestAccepted = 
                    await SubmitAndAcceptFulfillmentRequest(
                        order.Id, 
                        order.RequestStatus,
                        shopifyFulfillmentRequestClient);

                //We cannot create fulfillment before accepting fulfillment request.
                if (!isFulfillmentRequestAccepted)
                {
                    isSuccess = false;
                    continue;
                }


                var createFulfillmentResponse = await shopifyOrderFulfillmentClient.Create(
                    new FulfillmentShipping
                    {
                        FulfillmentRequestOrderLineItems = 
                            new List<LineItemsByFulfillmentOrder>() 
                            {
                                new POD.Integrations.ShopifyClient.Model.LineItemsByFulfillmentOrder
                                {
                                    FulfillmentOrderId = order.Id.GetValueOrDefault(),
                                    FulfillmentRequestOrderLineItems = shippedLineItems
                                }
                            },
                        NotifyCustomer = shopifyCreateFulfillmentMessage.NotifyEndCustomer,
                        TrackingInfo = 
                            new POD.Integrations.ShopifyClient.Model.TrackingInfo
                            {
                                Company = shopifyCreateFulfillmentMessage.TrackingInformation.ShippingCompany,
                                Number = shopifyCreateFulfillmentMessage.TrackingInformation.TrackingNumber,
                                Url = shopifyCreateFulfillmentMessage.TrackingInformation.TrackingUrl
                            }                    
                    });

                isSuccess = isSuccess && createFulfillmentResponse.IsSuccessful;

                if (!createFulfillmentResponse.IsSuccessful)
                {
                    logger.LogError($"Cannot create fulfillment for fulfillmentOrderId = {order.Id}");
                }
            }

            if (isSuccess)
            {
                await serviceBusService.SendChangeOrderStatusAsFulfilledMessage(shopifyCreateFulfillmentMessage.PodOrderId);
            }

            return isSuccess;
        }

        private async Task<bool> SubmitAndAcceptFulfillmentRequest(
                    long? fulfillmentOrderId,
                    string fulfillmentOrderRequestStatus, 
                    IShopifyFulfillmentRequestClient shopifyFulfillmentRequestClient)
        {
            if (fulfillmentOrderRequestStatus.Equals(
                    FulfillmentOrderRequestStatuses.ACCEPTED,
                    System.StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var isFulfillmentRequestSubmitted =
                await CreateFulfillmentRequestAsync(
                    fulfillmentOrderId,
                    fulfillmentOrderRequestStatus,
                    shopifyFulfillmentRequestClient);

            //We cannot submit fulfillment request or fulfillment request is cancelled. We cannot accept it.
            if (!isFulfillmentRequestSubmitted) return false;

            return await AcceptFulfillmentRequest(fulfillmentOrderId, shopifyFulfillmentRequestClient);
        }   

        private IEnumerable<FulfillmentRequestOrderLineItem> GetShippedLineItems(
            IEnumerable<FulfillmentOrderLineItem> fulfillmentOrderLineItems, 
            List<ShopifyOrderLineItem> shopifyOrderLineItems)
        {
            var matchedItems = fulfillmentOrderLineItems.Join(
                shopifyOrderLineItems,
                foli => foli.LineItemId,
                soli => soli.ShopifyOrderLineItemId,
                (foli, soli) => new
                {
                    FulfillmentRequestLineItems = new FulfillmentRequestOrderLineItem
                    {
                        Id = foli.Id,
                        Quantity =
                        soli.Quantity <= foli.Quantity
                        ? soli.Quantity
                        : foli.Quantity
                    },
                    ShopifyOrderLineItems = soli
                })
                .ToList();

            //We are removing the fulfilled items from the list
            //So if two different fulfillment order contains the same item we will not fulfill it twice
            foreach (var item in matchedItems.Select(mi => mi.ShopifyOrderLineItems))
            {
                shopifyOrderLineItems.Remove(item);
            } 

            return matchedItems.Select(mi => mi.FulfillmentRequestLineItems).ToList();
        }
    }
}
