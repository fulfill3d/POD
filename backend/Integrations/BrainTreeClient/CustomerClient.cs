using AutoMapper;
using Braintree;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using POD.Integrations.BrainTreeClient.Exceptions;
using POD.Integrations.BrainTreeClient.Interface;
using POD.Integrations.BrainTreeClient.Model.Customer;
using POD.Integrations.BrainTreeClient.Options;

namespace POD.Integrations.BrainTreeClient
{
    internal class CustomerClient(IOptions<BraintreeClientOptions> options, IMapper mapper, ILogger<CustomerClient> logger) : BaseClient(options), ICustomerClient
	{
        public async Task<CustomerResult> CreateCustomer(CreateCustomerRequest request)
		{
			Result<Customer> result = await Gateway.Customer.CreateAsync(mapper.Map<CustomerRequest>(request));

            if (result.IsSuccess())
            {
                logger.LogInformation($"Braintree customer successfully created with id:{result.Target.Id}");
                return mapper.Map<CustomerResult>(result.Target);
            }
            else
            {
                logger.LogError($"An error occured while creating payment method: {result.Message}");
                throw new BraintreeException(nameof(CreateCustomer), result.Message);
            }
		}

		public async Task<GenerateClientTokenResult> GenerateClientToken(string customerId)
		{
			var clientToken = await Gateway.ClientToken.GenerateAsync(new Braintree.ClientTokenRequest
			{
				CustomerId = customerId
			});

			return new GenerateClientTokenResult
			{
				ClientToken = clientToken
			};

        }
	}
}

