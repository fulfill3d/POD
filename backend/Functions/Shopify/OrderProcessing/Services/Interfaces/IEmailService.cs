namespace POD.Functions.Shopify.OrderProcessing.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendErrorToDevelopers(string errorReason, string orderData);
    }
}
