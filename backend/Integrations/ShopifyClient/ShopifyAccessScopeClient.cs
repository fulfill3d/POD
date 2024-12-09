using Microsoft.Extensions.Logging;
using POD.Integrations.ShopifyClient.Interface;
using POD.Integrations.ShopifyClient.Model;
using RestSharp;

namespace POD.Integrations.ShopifyClient
{
    public class ShopifyAccessScopeClient(string shop, string token, ILogger logger) : ShopifyBaseClient(shop, token, logger), IShopifyAccessScopeClient
    {
        protected override bool SupportsApiVersioning => false;

        public async Task<RestResponse<IEnumerable<AccessScope>>> GetAll()
        {
            var url = "oauth/access_scopes";
            var request = CreateRequest(url, Method.Get);

            return await base.ExecuteWithPolicyAsync<IEnumerable<AccessScope>>(request);
        }
    }
}