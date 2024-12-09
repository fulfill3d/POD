using System;
using Braintree;

namespace POD.Integrations.BrainTreeClient.Model.Transaction
{
	public class TransactionResult
	{
        public string Id { get; set; }
        public  DateTime? CreatedAt { get; set; }
    }
}

