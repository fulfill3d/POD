using POD.API.Seller.Payment.Data.Models;

namespace POD.API.Seller.Payment.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> DeleteCustomerPaymentMethod(Guid userId, int customerPaymentMethodId);
        Task<ICollection<SellerPaymentMethod>> GetCustomerPaymentMethods(Guid userId);
        Task<SellerPaymentMethod> GetDefaultPaymentMethod(Guid userId);

        Task<bool> SetDefaultCustomerPaymentMethod(Guid userId, int customerPaymentMethodId);
    }
}