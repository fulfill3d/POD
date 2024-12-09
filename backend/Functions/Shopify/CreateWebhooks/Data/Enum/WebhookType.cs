using System.ComponentModel;

namespace POD.Functions.Shopify.CreateWebhooks.Data.Enum
{
    public enum WebhookType
    {
        [Description("app/uninstalled")]
        AppUninstalled,
        [Description("orders/create")]
        OrdersCreate,
        [Description("orders/cancelled")]
        OrdersCancelled,
        [Description("orders/delete")]
        OrdersDelete,
        [Description("products/delete")]
        ProductsDelete,
        [Description("shop/update")]
        ShopUpdate,
    }
}