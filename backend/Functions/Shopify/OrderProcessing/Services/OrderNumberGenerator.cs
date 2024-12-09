using POD.Functions.Shopify.OrderProcessing.Services.Interfaces;

namespace POD.Functions.Shopify.OrderProcessing.Services
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        public string GenerateOrderNumber(int customerSaleOrderId)
        {
            var alphabet = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
            var stack = new Stack<char>();
            while (customerSaleOrderId > 0)
            {
                stack.Push(alphabet[customerSaleOrderId % alphabet.Length]);
                customerSaleOrderId /= alphabet.Length;
            }
            var output = new string(stack.ToArray()).PadLeft(7, '0');
            return output;
        }
    }
}
