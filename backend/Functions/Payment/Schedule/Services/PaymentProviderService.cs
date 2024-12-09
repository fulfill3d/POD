using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Schedule.Data.Database;
using POD.Functions.Payment.Schedule.Services.Interfaces;
using PaymentMethod = POD.Common.Core.Enum.PaymentMethod;

namespace POD.Functions.Payment.Schedule.Services
{
    public class PaymentProviderService(ScheduleContext dbContext) : IPaymentProviderService
    {
        
        public async Task<ChargePaymentMessage<PaymentDetails>> CreateChargePaymentMessage(
            int storeId, 
            int sellerPaymentMethodId, 
            decimal totalCost, 
            PaymentTransaction paymentTransaction)
        {
            var sellerPaymentMethod = await dbContext.SellerPaymentMethods
                 .Include(cpm => cpm.StripeDetails)
                 .Include(cpm => cpm.PaypalDetails)
                 .Include(cpm => cpm.BraintreeDetails)
                 .FirstOrDefaultAsync(t => t.Id == sellerPaymentMethodId);

            return new ChargePaymentMessage<PaymentDetails>
            {
                Currency = "USD",
                PaymentMethodId = sellerPaymentMethod.PaymentMethodId,
                SellerId = sellerPaymentMethod.SellerId,
                StoreId = storeId,
                PaymentTransaction = paymentTransaction,
                TotalCost = totalCost,
                SellerPaymentMethodId = sellerPaymentMethodId,
                PaymentDetails = GetPaymentDetails(sellerPaymentMethod)
            };
        }

        private PaymentDetails GetPaymentDetails(SellerPaymentMethod customerPaymentMethod)
        {
            return customerPaymentMethod.PaymentMethodId switch
            {
                (int)PaymentMethod.Stripe => new StripeDetails
                {
                    StripeSellerId = customerPaymentMethod.StripeDetails.FirstOrDefault().StripeSellerId,
                    StripePaymentId = customerPaymentMethod.StripeDetails.FirstOrDefault().StripePaymentMethodId
                },
                (int)PaymentMethod.Braintree => new BraintreeDetails
                {
                    BraintreeSellerId = customerPaymentMethod.BraintreeDetails.FirstOrDefault().BraintreeSellerId,
                    DeviceData = customerPaymentMethod.BraintreeDetails.FirstOrDefault().DeviceData,
                    PaymentMethodNonce = customerPaymentMethod.BraintreeDetails.FirstOrDefault().Token
                },
                _ => throw new Exception(),
            };
        }

        
    }
}