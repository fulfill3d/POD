using POD.Common.Database.Models;

namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IEmailService
    {
        Task NotifyStorePaymentError(Store store);
    }
}
