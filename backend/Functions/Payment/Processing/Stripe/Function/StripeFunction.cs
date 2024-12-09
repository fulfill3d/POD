using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Stripe.Services.Interfaces;

namespace POD.Functions.Payment.Processing.Stripe
{
    public class StripeFunction(IStripeService stripeService)
    {
        [Function("payment-call-executes-stripe")]
        public async Task Run(
            [ServiceBusTrigger("payment-call-executes-stripe", Connection = "ServiceBusConnectionString", IsSessionsEnabled = true)] string message,
            ILogger logger)
        {
            logger.LogInformation("Stripe Call Execute processing started.");

            var messageObj = JsonConvert.DeserializeObject<ChargePaymentMessage<StripeDetails>>(message);
            await stripeService.ChargeStripe(messageObj);

            logger.LogInformation("Stripe Call Execute processing finished.");
        }
    }
}