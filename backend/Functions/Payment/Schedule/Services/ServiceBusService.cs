using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Common.Core.Enum;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Schedule.Services.Interfaces;
using POD.Functions.Payment.Schedule.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Payment.Schedule.Services
{
    public class ServiceBusService(
        IServiceBusClient serviceBusClient,
        IOptions<QueueNames> queueNames
    ) : IServiceBusService
    {
        private readonly string _stripeCallExecutesQueueName = queueNames.Value.StripeCallExecutesQueueName;
        private readonly string _braintreeCallExecutesQueueName = queueNames.Value.BraintreeCallExecutesQueueName;

        public async Task SendChargePaymentMessage(ChargePaymentMessage<PaymentDetails> chargePaymentMessage)
        {
            var queueName = GetQueueName(chargePaymentMessage.PaymentMethodId);

            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(chargePaymentMessage),
                SessionId =  queueName,
                QueueName = queueName
            });
        }

        private string GetQueueName(int paymentMethodId)
        {
            return paymentMethodId switch
            {
                (int)PaymentMethod.Stripe => _stripeCallExecutesQueueName,
                (int)PaymentMethod.Braintree => _braintreeCallExecutesQueueName,
                _ => throw new Exception(),
            };
        }
    }
}
