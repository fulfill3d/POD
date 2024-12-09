using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Stripe.Services.Interfaces;
using POD.Functions.Payment.Processing.Stripe.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Payment.Processing.Stripe.Services
{
    public class ServiceBusService(IOptions<QueueNames> queueNames, IServiceBusClient serviceBusClient) : IServiceBusService
    {
        private readonly string _stripePaymentResultsQueueName = queueNames.Value.StripePaymentResultsQueueName;

        public async Task SendStripePaymentResultMessage(PaymentResultMessage<StripeResultDetails> paymentResultMessage)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(paymentResultMessage),
                QueueName = _stripePaymentResultsQueueName
            });
        }
    }
}
