using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing
{
    public class BraintreePaymentResultProcessing(IBraintreePaymentResultService braintreePaymentResultService)
	{
        
        [Function("payment-result-braintree")]
        public async Task Run(
           [ServiceBusTrigger("payment-result-braintree", 
               Connection = "ServiceBusConnectionString",
               IsSessionsEnabled = false)] string message,
           ILogger logger)
        {
            logger.LogInformation("Braintree Payment Result processing started.");

            var messageObj = JsonConvert.DeserializeObject<PaymentResultMessage<BraintreeResultDetails>>(message);
            await braintreePaymentResultService.SaveBraintreePaymentResult(messageObj);

            logger.LogInformation("Braintree Payment Result processing finished.");
        }
    }
}

