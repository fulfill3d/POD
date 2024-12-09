using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.API.Seller.Payment.Data.Database;
using POD.API.Seller.Payment.Data.Models.Braintree;
using POD.API.Seller.Payment.Services.Interfaces;
using POD.API.Seller.Payment.Services.Options;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;
using POD.Integrations.BrainTreeClient.Interface;
using POD.Integrations.BrainTreeClient.Model.Customer;
using PaymentMethod = POD.Common.Core.Enum.PaymentMethod;

namespace POD.API.Seller.Payment.Services
{
    public class BraintreeService(
        ICustomerClient customerClient,
        ICommonUserService commonUserService,
        PaymentContext dbContext,
        ILogger<BraintreeService> logger,
        IPaymentMethodClient paymentMethodClient,
        IOptions<PaymentOptions> paymentManagementOptions): IBraintreeService
    {
        private readonly string _waitingForConfirmationString = paymentManagementOptions.Value.WaitingForConfirmationString;
        
        public async Task<GenerateClientTokenResult> GetClientToken(Guid userRefId)
		{
            var braintreeSellerId = await dbContext.BraintreeDetails
                .Where(t => t.SellerPaymentMethod.Seller.UserRefId == userRefId)
                .Select(t => t.BraintreeSellerId)
                .FirstOrDefaultAsync();
            
            var user = await commonUserService.GetUserByUserRefId(userRefId);
            
            if (braintreeSellerId == null)
            {
                logger.LogInformation($"Braintree account related to the customer could not be found. Creating a new braintree account.");

                var braintreeCustomer = await customerClient.CreateCustomer(new CreateCustomerRequest
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });

                braintreeSellerId = braintreeCustomer.Id;
                logger.LogInformation($"Braintree account created sucessfully.User ID:{user.RefId}, Braintree Customer ID: {braintreeSellerId}");
            }

            var clientTokenResult = await customerClient.GenerateClientToken(braintreeSellerId);

            dbContext.BraintreeDetails.Add(new BraintreeDetail
            {
                CardholderName = $"{user.FirstName} {user.LastName}",
                CreatedDate = DateTime.UtcNow,
                IsEnabled = false,
                LastModifiedDate = DateTime.UtcNow,
                BraintreeSellerId = braintreeSellerId,
                ClientToken = clientTokenResult.ClientToken,
                BillingAgreementId = _waitingForConfirmationString,
                DeviceData = _waitingForConfirmationString,
                Token = _waitingForConfirmationString,
                Tenant = _waitingForConfirmationString,
                Type = _waitingForConfirmationString,
                PlaceHolder = _waitingForConfirmationString,
                SellerPaymentMethod = new SellerPaymentMethod
                {
                    CreatedDate = DateTime.UtcNow,
                    SellerId = await dbContext.Sellers
                        .Where(s => s.IsEnabled && s.UserRefId == userRefId)
                        .Select(s => s.Id)
                        .FirstOrDefaultAsync(),
                    IsDefault = false,
                    IsEnabled = false,
                    PaymentMethodId = (int)PaymentMethod.Braintree,
                    LastModifiedDate = DateTime.UtcNow,
                }
            });

            await dbContext.SaveChangesAsync();
            return clientTokenResult;
        }

        public async Task<Data.Models.SellerPaymentMethod> CompleteSetup(Guid userId, CompleteSetupRequest request)
        {
            var anyDefaultMethod = dbContext.SellerPaymentMethods
                .Any(t => t.Seller.UserRefId == userId && t.Seller.IsEnabled && t.IsDefault && t.IsEnabled);

            var braintreeDetail = dbContext.BraintreeDetails
                .Include(sd => sd.SellerPaymentMethod)
                .FirstOrDefault(t => t.ClientToken == request.ClientToken && t.SellerPaymentMethod.Seller.UserRefId == userId);

            var paymentMethodResult = await paymentMethodClient
                .CreatePaymentMethod(braintreeDetail.BraintreeSellerId, request.Nonce);

            braintreeDetail.BillingAgreementId = request.Details.BillingAgreementId;
            braintreeDetail.DeviceData = request.DeviceData;
            braintreeDetail.Token = paymentMethodResult.Token;
            braintreeDetail.Tenant = request.Details.Tenant;
            braintreeDetail.Type = request.Type;
            braintreeDetail.PlaceHolder = request.Details.Email;
            braintreeDetail.CardholderName = request.Details.FirstName + " " + request.Details.LastName;

            braintreeDetail.IsEnabled = true;
            braintreeDetail.LastModifiedDate = DateTime.UtcNow;
            braintreeDetail.SellerPaymentMethod.IsEnabled = true;
            braintreeDetail.SellerPaymentMethod.IsDefault = !anyDefaultMethod;
            braintreeDetail.LastModifiedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return new Data.Models.SellerPaymentMethod(
                            braintreeDetail.SellerPaymentMethodId,
                            (int)PaymentMethod.Braintree,
                            braintreeDetail.SellerPaymentMethod.IsDefault,
                            braintreeDetail.PlaceHolder,
                            braintreeDetail.CardholderName);
        }
    }
}