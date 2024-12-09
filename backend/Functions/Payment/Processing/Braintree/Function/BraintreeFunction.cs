using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Braintree.Services.Interfaces;

namespace POD.Functions.Payment.Processing.Braintree
{
    public class BraintreeFunction(IBraintreeService braintreeService)
    {
        [Function("payment-call-executes-braintree")]
        public async Task Run(
            [ServiceBusTrigger("payment-call-executes-braintree", 
                Connection = "ServiceBusConnectionString",
                IsSessionsEnabled = true)] string message,
            ILogger logger)
        {
            logger.LogInformation("Braintree Call Execute processing started.");

            var messageObj = JsonConvert.DeserializeObject<ChargePaymentMessage<BraintreeDetails>>(message);
            await braintreeService.ChargeBraintree(messageObj);

            logger.LogInformation("Braintree Call Execute processing finished.");
        }
    }
}