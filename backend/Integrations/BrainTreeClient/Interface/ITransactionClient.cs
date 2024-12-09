using POD.Integrations.BrainTreeClient.Model.Transaction;

namespace POD.Integrations.BrainTreeClient.Interface
{
    public interface ITransactionClient
    {
        Task<TransactionResult> CreateTransaction(decimal amount, string nonce, string deviceData, string currency);
    }
}