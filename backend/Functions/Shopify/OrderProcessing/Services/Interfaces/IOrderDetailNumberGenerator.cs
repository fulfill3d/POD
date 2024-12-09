namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IOrderDetailNumberGenerator
    {
        public string GenerateOrderDetailNumber(int index, string orderNumber);
    }
}
