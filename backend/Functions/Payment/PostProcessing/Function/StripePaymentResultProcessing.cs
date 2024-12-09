using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.PostProcessing.Services.Interfaces;

namespace POD.Functions.Payment.PostProcessing
{
    public class StripePaymentResultProcessing(IStripePaymentResultService stripePaymentResultService)
    {

        [FunctionName("payment-result-stripe")]
        public async Task Run(
            [ServiceBusTrigger("payment-result-stripe", 
                Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = false)]string message, 
            ILogger logger)
        {
            logger.LogInformation("Stripe Payment Result processing started.");

            var messageObj = JsonConvert.DeserializeObject<PaymentResultMessage<StripeResultDetails>>(message);
            await stripePaymentResultService.SaveStripePaymentResult(messageObj);

            logger.LogInformation("Stripe Payment Result processing finished.");
        }
    }
}
