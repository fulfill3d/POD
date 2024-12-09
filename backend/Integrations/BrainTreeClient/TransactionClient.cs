using AutoMapper;
using Braintree;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Integrations.BrainTreeClient.Exceptions;
using POD.Integrations.BrainTreeClient.Interface;
using POD.Integrations.BrainTreeClient.Model.Transaction;
using POD.Integrations.BrainTreeClient.Options;

namespace POD.Integrations.BrainTreeClient
{
    internal class TransactionClient(IOptions<BraintreeClientOptions> options, ILogger<TransactionClient> logger, IMapper mapper) : BaseClient(options), ITransactionClient
    {

        public async Task<TransactionResult> CreateTransaction(decimal amount, string nonce, string deviceData, string currency)
        {
            TransactionRequest request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodToken = nonce,
                DeviceData = deviceData,
                CurrencyIsoCode = currency,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };

            Result<Transaction> result = await Gateway.Transaction.SaleAsync(request);

            if (result.IsSuccess())
            {
                logger.LogInformation($"Braintree transaction successfully created. Transaction Id:{result.Target.Id}");
                return mapper.Map<TransactionResult>(result.Target);
            }
            else
            {
                logger.LogError($"An error occured while creating transaction: {result.Message}");
                throw new BraintreeException(nameof(CreateTransaction), result.Message);
            }
        }
    }
}

