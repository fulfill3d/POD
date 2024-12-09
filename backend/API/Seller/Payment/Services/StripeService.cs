using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.API.Seller.Payment.Data.Database;
using POD.API.Seller.Payment.Data.Models.Stripe;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.API.Seller.Payment.Services.Options;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using POD.Integrations.StripeClient.Interface;
using POD.Integrations.StripeClient.Model.Customer;
using POD.Integrations.StripeClient.Model.SetupIntent;
using PaymentMethod = POD.Common.Core.Enum.PaymentMethod;

namespace POD.API.Seller.Payment.Services
{
    public class StripeService(
        PaymentContext dbContext,
        ISetupIntentClient setupIntentClient,
        ICustomerClient customerClient,
        ILogger<StripeService> logger,
        IPaymentMethodClient paymentMethodClient,
        ICommonUserService commonUserService,
        IOptions<PaymentOptions> paymentManagementOptions) : IStripeService
    {
        private readonly string _waitingForConfirmationString = paymentManagementOptions.Value.WaitingForConfirmationString;
        
        public async Task<SetupIntentResult> CreateSetupIntent(Guid userRefId)
        {
            var stripeSellerId = await dbContext.StripeDetails
                .Where(t => t.SellerPaymentMethod.Seller.UserRefId == userRefId)
                .Select(t => t.StripeSellerId)
                .FirstOrDefaultAsync();

            var user = await commonUserService.GetUserByUserRefId(userRefId);

            if (stripeSellerId == null)
            {
                logger.LogInformation($"Stripe account related to the customer could not be found. Creating a new stripe account.");

                CreateCustomerRequest createCustomerRequest = new CreateCustomerRequest
                {
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Phone = user.Phone
                };

                var stripeSeller = customerClient.CreateCustomer(createCustomerRequest);
                stripeSellerId = stripeSeller.Id;

                logger.LogInformation($"Stripe account created sucessfully.User ID:{user.RefId}, Stripe Seller ID: {stripeSellerId}");
            }

            var setupIntentResult = setupIntentClient.SetupCardIntent(stripeSellerId);

            dbContext.StripeDetails.Add(new StripeDetail
            {
                CreatedDate = DateTime.UtcNow,
                IsEnabled = false,
                LastModifiedDate = DateTime.UtcNow,
                StripeSellerId = stripeSellerId,
                StripeSetupIntentId = setupIntentResult.Id,
                ExpireDate = _waitingForConfirmationString,
                StripePaymentMethodId = _waitingForConfirmationString,
                PlaceHolder = _waitingForConfirmationString,
                CardholderName = $"{user.FirstName} {user.LastName}",
                SellerPaymentMethod = new SellerPaymentMethod
                {
                    CreatedDate = DateTime.UtcNow,
                    SellerId = await dbContext.Sellers
                        .Where(s => s.IsEnabled && s.UserRefId == userRefId)
                        .Select(s => s.Id)
                        .FirstOrDefaultAsync(),
                    IsDefault = false,
                    IsEnabled = false,
                    PaymentMethodId = (int)PaymentMethod.Stripe,
                    LastModifiedDate = DateTime.UtcNow,
                }
            });

            await dbContext.SaveChangesAsync();
            return setupIntentResult;
        }



        public async Task<Data.Models.SellerPaymentMethod> CompleteSetup(Guid userId, CompleteSetupRequest intent)
        {
            var anyDefaultMethod = dbContext.SellerPaymentMethods
                .Any(t => t.Seller.IsEnabled && t.Seller.UserRefId == userId && t.IsDefault && t.IsEnabled);

            var paymentMethod = paymentMethodClient.GetPaymentMethod(intent.PaymentMethodId);

            var stripeDetail = dbContext.StripeDetails
                .Include(sd=>sd.SellerPaymentMethod)
                .FirstOrDefault(t => t.StripeSetupIntentId == intent.Id && t.SellerPaymentMethod.Seller.UserRefId == userId);
            
            stripeDetail.StripePaymentMethodId = intent.PaymentMethodId;
            stripeDetail.PlaceHolder = $"{paymentMethod.Card.Country} - {paymentMethod.Card.Brand}/{paymentMethod.Card.Last4}";
            stripeDetail.IsEnabled = true;
            stripeDetail.LastModifiedDate = DateTime.UtcNow;
            stripeDetail.SellerPaymentMethod.IsEnabled = true;
            stripeDetail.SellerPaymentMethod.IsDefault = !anyDefaultMethod;
            stripeDetail.LastModifiedDate = DateTime.UtcNow;
            stripeDetail.CardholderName = "paymentMethod.Card.Name"; // TODO CardholderName should be nullable
            stripeDetail.ExpireDate = paymentMethod.Card.ExpireMonth + "/" + paymentMethod.Card.ExpireYear;

            await dbContext.SaveChangesAsync();

            return new Data.Models.SellerPaymentMethod(
                            stripeDetail.SellerPaymentMethodId,
                            (int)PaymentMethod.Stripe,
                            stripeDetail.SellerPaymentMethod.IsDefault,
                            stripeDetail.PlaceHolder,
                            stripeDetail.ExpireDate,
                            stripeDetail.CardholderName);
        }

    }
}