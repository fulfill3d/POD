using Microsoft.EntityFrameworkCore;
using POD.API.Common.Core.Exception;
using POD.API.Seller.Payment.Data.Database;
using POD.API.Seller.Payment.Data.Models;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.Common.Core.Enum;
using POD.Integrations.StripeClient.Interface;

namespace POD.API.Seller.Payment.Services
{
    public class PaymentService(PaymentContext db, 
        IPaymentMethodClient paymentMethodClient) : IPaymentService
    {

        public async Task<bool> DeleteCustomerPaymentMethod(Guid userId, int customerPaymentMethodId)
        {
            var entity = await db.SellerPaymentMethods
                .Include(t => t.StripeDetails).AsSplitQuery()
                .Include(t => t.PaypalDetails).AsSplitQuery()
                .Where(t => t.Seller.UserRefId == userId && t.Id == customerPaymentMethodId)
                .FirstOrDefaultAsync();


            if (entity == null) return false;
            entity.IsEnabled = false;
            entity.LastModifiedDate = DateTime.UtcNow;

            if (entity.IsDefault)
            {
                var newDefault = await db.SellerPaymentMethods
                .Where(t => t.Id != customerPaymentMethodId && t.SellerId == entity.SellerId && t.IsEnabled)
                .FirstOrDefaultAsync();

                if (newDefault != null) newDefault.IsDefault = true;
            }

            await db.SaveChangesAsync();

            if (entity.PaymentMethodId == (int)PaymentMethod.Stripe)
            {
                paymentMethodClient.Detach(entity.StripeDetails.First().StripePaymentMethodId);
            }

            return true;
        }

        public async Task<ICollection<SellerPaymentMethod>> GetCustomerPaymentMethods(Guid userId)
        {
            return await db.SellerPaymentMethods
                    .Include(t => t.PaypalDetails)
                        .AsSplitQuery()
                    .Include(t => t.StripeDetails)
                        .AsSplitQuery()
                    .Include(t=>t.BraintreeDetails)
                        .AsSplitQuery()
                    .Where(c => c.IsEnabled == true && c.Seller.IsEnabled && c.Seller.UserRefId == userId)
                    .Select(m =>
                        new SellerPaymentMethod(
                            m.Id,
                            m.PaymentMethodId,
                            m.IsDefault,
                            m.GetPlaceHolder(),
                            m.GetExpireDate(),
                            m.GetCardholderName()))
                    .ToListAsync();
        }


        public async Task<SellerPaymentMethod> GetDefaultPaymentMethod(Guid userId)
        {
            var method = await db.SellerPaymentMethods
                    .Include(t => t.PaypalDetails)
                        .AsSplitQuery()
                    .Include(t => t.StripeDetails)
                        .AsSplitQuery()
                    .Include(t => t.BraintreeDetails)
                        .AsSplitQuery()
                    .Where(c => c.IsEnabled == true && c.Seller.IsEnabled && c.IsDefault && c.Seller.UserRefId == userId)
                    .Select(m =>
                        new SellerPaymentMethod(
                            m.Id,
                            m.PaymentMethodId,
                            m.IsDefault,
                            m.GetPlaceHolder(),
                            m.GetExpireDate(),
                            m.GetCardholderName()))
                    .FirstOrDefaultAsync();
            
            if (method == null) throw new EntityNotFoundException();

            return method;
        }

        public async Task<bool> SetDefaultCustomerPaymentMethod(Guid userId, int customerPaymentMethodId)
        {
            var newDefault = await db.SellerPaymentMethods
                .Where(t => t.IsEnabled && t.Id == customerPaymentMethodId)
                .FirstOrDefaultAsync();
            if (newDefault == null) return false;

            //There must only be one default payment method. However, ToList was used instead of FirstOrDefault just in case.
            var oldDefaults = await db.SellerPaymentMethods
                .Where(t => t.IsDefault && t.Seller.UserRefId == userId && t.Seller.IsEnabled && t.IsEnabled)
                .ToListAsync();

            newDefault.IsDefault = true;
            newDefault.LastModifiedDate = DateTime.UtcNow;

            oldDefaults.ForEach(e =>
            {
                e.IsDefault = false;
                e.LastModifiedDate = DateTime.UtcNow;
            });

            await db.SaveChangesAsync();

            return true;
        }
    }
}