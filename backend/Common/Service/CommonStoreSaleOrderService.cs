using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;
using POD.Common.Service.Interfaces;

namespace POD.Common.Service
{
    public class CommonStoreSaleOrderService(DatabaseContext dbContext): ICommonStoreSaleOrderService
    {
        public async Task ChangeStoreSaleOrderStatus(int storeSaleOrderId, Common.Core.Enum.OrderStatus status)
        {
            var saleOrderStatus = await GetStoreSaleOrderStatusByStoreSaleId(storeSaleOrderId);

            if (saleOrderStatus == null) return; // TODO Exception

            saleOrderStatus.OrderStatusId = (int)status;
            saleOrderStatus.UpdatedAt = DateTime.UtcNow;
            saleOrderStatus.ProcessCount++; // TODO Maybe not HERE

           await dbContext.SaveChangesAsync();

        }

        private async Task<StoreSaleOrderStatus?> GetStoreSaleOrderStatusByStoreSaleId(int storeSaleOrderId)
        {
            return await dbContext.StoreSaleOrderStatuses
                .FirstOrDefaultAsync(sso => sso.IsEnabled == true && sso.StoreSaleOrderId == storeSaleOrderId);
        }
    }
}