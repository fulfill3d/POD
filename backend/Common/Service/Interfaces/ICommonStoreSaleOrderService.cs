using POD.Common.Database.Models;

namespace POD.Common.Service.Interfaces
{
    public interface ICommonStoreSaleOrderService
    {
        Task ChangeStoreSaleOrderStatus(int storeSaleOrderId, Common.Core.Enum.OrderStatus status);
    }
}