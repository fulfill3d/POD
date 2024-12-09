using POD.Common.Database.Models;
using POD.Functions.Payment.Common.Data.Models;

namespace POD.Functions.Payment.Schedule.Services.Interfaces
{
    public interface IStoreSaleTransactionService
    {
        SaleTransaction CreateStoreSaleTransactionOfStoreSaleOrder(StoreSaleOrder order);
    }
}
