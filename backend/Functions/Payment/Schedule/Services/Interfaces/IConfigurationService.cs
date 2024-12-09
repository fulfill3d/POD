namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<string> GetDefaultPaymentMethodNotFoundMailMessageAsync();
    }
}
