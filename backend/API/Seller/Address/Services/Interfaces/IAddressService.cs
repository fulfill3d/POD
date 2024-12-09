namespace POD.API.Seller.Address.Services.Interfaces
{
    public interface IAddressService
    {
        Task<IEnumerable<Data.Models.Address>> GetAddresses(int sellerId);
        Task<Data.Models.Address> AddAddress(int sellerId, Data.Models.Address address);
        Task<Data.Models.Address?> GetAddress(int sellerId, int sellerAddressId);
        Task<Data.Models.Address?> UpdateAddress(int sellerId, Data.Models.Address address);
        Task<bool> DeleteAddress(int sellerId, int sellerAddressId);
    }
}