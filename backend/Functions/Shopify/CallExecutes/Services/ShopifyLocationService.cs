using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Functions.Shopify.CallExecutes.Data.Models;
using POD.Functions.Shopify.CallExecutes.Services.Interfaces;
using POD.Functions.Shopify.CallExecutes.Services.Options;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.CallExecutes.Services
{
    public class ShopifyLocationService(
        IShopifyLocationClientFactory shopifyLocationClientFactory,
        IServiceBusService serviceBusService,
        IOptions<LocationConfig> locationConfigOpt,
        ILogger<ShopifyLocationService> logger) : IShopifyLocationService
    {
        private readonly LocationConfig _locationConfig = locationConfigOpt.Value;

        public async Task<bool> GetLocationIdForOrderFulfillment(ShopifyGetLocationIdForOrderFulfillmentMessage shopifyGetLocationIdMessage)
        {
            var location = await GetPodLocationAsync(shopifyGetLocationIdMessage.Shop, shopifyGetLocationIdMessage.Token);

            if (location == null) return false;

            var message =
                new ShopifyFulfillOrdersByCustomerMessage
                {
                    CustomerId = shopifyGetLocationIdMessage.CustomerId,
                    Shop = shopifyGetLocationIdMessage.Shop,
                    Token = shopifyGetLocationIdMessage.Token,
                    LocationId = location.Id.GetValueOrDefault()
                };

            await serviceBusService.SendFulfillOrdersByCustomerMessage(message);

            return true;
        }

        public async Task<Location?> GetPodLocationAsync(string shop, string token)
        {
            var shopifyLocationClient = shopifyLocationClientFactory.CreateClient(shop, token);

            var locationsResponse = await shopifyLocationClient.GetAll();

            if (!locationsResponse.IsSuccessful)
            {
                logger.LogError($"Failed to get locations for customer");
                return null;
            }

            var podLocation = locationsResponse.Data?.FirstOrDefault(x => x.Name == _locationConfig.PodFulfilmentServiceName);

            if (podLocation == null)
            {
                logger.LogError("Cannot find POD location");
                return null;
            }

            return podLocation;
        }


    }
}
