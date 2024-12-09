using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class OrderDetailNumberGenerator : IOrderDetailNumberGenerator
    {
        public string GenerateOrderDetailNumber(int index, string orderNumber)
        {
            return $"{"POD"}{orderNumber:D7}{"I"}{index:D3}";
        }
    }
}
