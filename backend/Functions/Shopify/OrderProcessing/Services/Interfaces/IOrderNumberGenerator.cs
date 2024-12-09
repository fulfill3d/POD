namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IOrderNumberGenerator
    {
        public string GenerateOrderNumber(int customerSaleOrderId);
    }
}
