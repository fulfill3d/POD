namespace POD.API.Seller.Common.Services.Interfaces
{
    public interface ICommonSellerService
    {
        Task<int> GetSellerIdByUserId(Guid userId);
        Task<int> GetSellerIdByShop(string shop, POD.Common.Core.Enum.MarketPlace marketPlace);
        Task<POD.Common.Database.Models.Seller> GetByShop(string shop);
        Task<POD.Common.Database.Models.Seller> GetByCustomerIdAsync(int customerId);
        Task<bool> IsCustomerBlocked(string shop);
        public Task<bool> SaveSellerStatusProcessing(POD.Common.Database.Models.Seller seller, string planName);
    }
}