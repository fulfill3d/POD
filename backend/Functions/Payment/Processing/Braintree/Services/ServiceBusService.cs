using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using POD.Functions.Payment.Common.Data.Models;
using POD.Functions.Payment.Processing.Braintree.Services.Interfaces;
using POD.Functions.Payment.Processing.Braintree.Services.Options;
using POD.Integrations.ServiceBusClient.Interfaces;
using POD.Integrations.ServiceBusClient.Models;

namespace POD.Functions.Payment.Processing.Braintree.Services
{
    public class ServiceBusService(IOptions<QueueNames> queueNames, 
        IServiceBusClient serviceBusClient) : IServiceBusService
    {
        private readonly string _braintreePaymentResultsQueueName = queueNames.Value.BraintreePaymentResultsQueueName;
        
        public async Task SendBraintreePaymentResultMessage(PaymentResultMessage<BraintreeResultDetails> paymentResultMessage)
        {
            await serviceBusClient.SendMessage(new ServiceBusClientMessage
            {
                Message = JsonConvert.SerializeObject(paymentResultMessage),
                QueueName = _braintreePaymentResultsQueueName
            });
        }
    }
}