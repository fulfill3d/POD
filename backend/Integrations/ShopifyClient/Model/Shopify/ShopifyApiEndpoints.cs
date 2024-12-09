namespace POD.Integrations.ShopifyClient.Model.Shopify
{
    public class ShopifyApiEndpoints(string shop, string appUrl, string apiKey)
    {
        public string AuthorizeUrl => $"https://{shop}/admin/oauth/authorize?client_id={apiKey}";

        public string RedirectUri => $"&redirect_uri=https://{appUrl}/api/webhooks/auth";

        public string FrontEndUrl => $"https://{shop}/admin/apps/{apiKey}";

        public string DefaultScope =>
            "&scope=read_orders,read_products,write_products,write_orders,read_fulfillments,write_fulfillments";

        public string InventoryScope =>
            "&scope=read_inventory" +
            ",write_inventory" +
            ",read_orders" +
            ",read_products" +
            ",write_products" +
            ",write_orders" +
            ",read_fulfillments" +
            ",write_fulfillments" +
            ",read_assigned_fulfillment_orders" +
            ",write_assigned_fulfillment_orders" +
            ",write_third_party_fulfillment_orders";
    }
}