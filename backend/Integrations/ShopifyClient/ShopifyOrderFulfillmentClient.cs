using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using POD.Integrations.ShopifyClient.Model.Order;
using RestSharp;
using System.Dynamic;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyOrderFulfillmentClient(
        string token,
        string store,
        ILogger<ShopifyOrderFulfillmentClient> logger) : ShopifyBasicClient<FulfillmentShipping>(token, store, "fulfillments", logger), IShopifyOrderFulfillmentClient
    {
        public new async Task<RestResponse<Fulfillment>> Create(FulfillmentShipping shipping)
        {
            var url = "fulfillments.json";

            // Create a dynamic object to wrap the fulfillment data
            dynamic obj = new ExpandoObject();
            obj.fulfillment = shipping;

            // Use CreateRequest to prepare the request
            var request = CreateRequest(url, Method.Post, obj);

            return await ExecuteWithPolicyAsync<Fulfillment>(request);
        }
    }
}