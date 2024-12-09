using Newtonsoft.Json.Linq;
using POD.Functions.Shopify.CallExecutes.Data.Models;

namespace POD.Functions.Shopify.CallExecutes.Services.Interfaces
{
    public interface IShopifyTaskService
    {
        Task<bool> ProcessShopifyCallExecuteAsync(ShopifyCallExecuteMessage<JObject> message);
    }
}
