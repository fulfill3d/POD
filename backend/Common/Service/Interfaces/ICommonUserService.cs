using POD.Common.Database.Models;

namespace POD.Common.Service.Interfaces
{
    public interface ICommonUserService
    {
        Task<User?> GetUserByUserRefId(Guid guid);
        Task<User?> GetUserBySellerId(int sellerId);
        Task<User?> GetUserByStoreId(int storeId);
    }
}