using Microsoft.EntityFrameworkCore;
using POD.API.Seller.Address.Data.Database;
using POD.API.Seller.Address.Services.Interfaces;
using POD.Common.Database.Models;

namespace POD.API.Seller.Address.Services
{
    public class AddressService(AddressContext dbContext) : IAddressService
    {
        
        public async Task<IEnumerable<Data.Models.Address>> GetAddresses(int sellerId)
        {
            var addresses =
                await dbContext.SellerAddresses
                    .Where(sa =>
                        sa.SellerId == sellerId
                        && sa.IsEnabled
                        && sa.Address.IsEnabled)
                    .Select(ca =>
                        new Data.Models.Address
                        {
                            Id = ca.AddressId,
                            SellerAddressId = ca.AddressId,
                            FirstName = ca.Address.FirstName,
                            LastName = ca.Address.LastName,
                            Street1 = ca.Address.Street1,
                            Street2 = ca.Address.Street2,
                            City = ca.Address.City,
                            State = ca.Address.State,
                            Country = ca.Address.Country,
                            ZipCode = ca.Address.ZipCode
                        })
                    .ToListAsync();

            return addresses;
        }

        public async Task<Data.Models.Address> AddAddress(int sellerId, Data.Models.Address address)
        {
            var newAddress = new POD.Common.Database.Models.Address
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                Street1 = address.Street1,
                Street2 = address.Street2,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow,
            };
            
            await dbContext.Addresses.AddAsync(newAddress);
            await dbContext.SaveChangesAsync();
            
            var sellerAddress = new SellerAddress
            {
                SellerId = sellerId,
                AddressId = newAddress.Id,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow
            };

            // TODO Double Save / Use HashSet instead when saving parent
            await dbContext.SellerAddresses.AddAsync(sellerAddress);
            await dbContext.SaveChangesAsync();

            return new Data.Models.Address
            {
                Id = newAddress.Id,
                SellerAddressId = sellerAddress.Id,
                FirstName = sellerAddress.Address.FirstName,
                LastName = sellerAddress.Address.LastName,
                Street1 = sellerAddress.Address.Street1,
                Street2 = sellerAddress.Address.Street2,
                City = sellerAddress.Address.City,
                State = sellerAddress.Address.State,
                Country = sellerAddress.Address.Country,
                ZipCode = sellerAddress.Address.ZipCode
            };
        }

        public async Task<Data.Models.Address?> GetAddress(int sellerId, int sellerAddressId)
        {
            var address =
                await dbContext.SellerAddresses
                    .Where(sa =>
                        sa.SellerId == sellerId
                        && sa.Id == sellerAddressId
                        && sa.IsEnabled
                        && sa.Address.IsEnabled)
                    .Select(sa =>
                        new Data.Models.Address
                        {
                            Id = sa.AddressId,
                            SellerAddressId = sa.Id,
                            FirstName = sa.Address.FirstName,
                            LastName = sa.Address.LastName,
                            Street1 = sa.Address.Street1,
                            Street2 = sa.Address.Street2 ?? "",
                            City = sa.Address.City,
                            State = sa.Address.State,
                            Country = sa.Address.Country,
                            ZipCode = sa.Address.ZipCode
                        })
                    .FirstOrDefaultAsync();

            return address;
        }

        public async Task<Data.Models.Address?> UpdateAddress(int sellerId, Data.Models.Address address)
        {
            var foundAddress =
                await dbContext.SellerAddresses
                    .Include(sa => sa.Address)
                    .Where(sa =>
                        sa.SellerId == sellerId
                        && sa.Id == address.SellerAddressId
                        && sa.IsEnabled
                        && sa.Address.IsEnabled)
                    .FirstOrDefaultAsync();
            if (foundAddress == null) return null;

            foundAddress.Address.FirstName = address.FirstName;
            foundAddress.Address.LastName = address.LastName;
            foundAddress.Address.Street1 = address.Street1;
            foundAddress.Address.Street2 = address.Street2;
            foundAddress.Address.City = address.City;
            foundAddress.Address.State = address.State;
            foundAddress.Address.Country = address.Country;
            foundAddress.Address.ZipCode = address.ZipCode;
            foundAddress.Address.LastModifiedDate = DateTime.UtcNow;
            foundAddress.LastModifiedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return new Data.Models.Address
            {
                Id = foundAddress.AddressId,
                SellerAddressId = foundAddress.Id,
                FirstName = foundAddress.Address.FirstName,
                LastName = foundAddress.Address.LastName,
                Street1 = foundAddress.Address.Street1,
                Street2 = foundAddress.Address.Street2,
                City = foundAddress.Address.City,
                State = foundAddress.Address.State,
                Country = foundAddress.Address.Country,
                ZipCode = foundAddress.Address.ZipCode
            };
        }

        public async Task<bool> DeleteAddress(int sellerId, int sellerAddressId)
        {
            var foundAddress =
                await dbContext.SellerAddresses
                    .Include(ca => ca.Address)
                    .Where(sa =>
                        sa.SellerId == sellerId
                        && sa.Id == sellerAddressId
                        && sa.IsEnabled
                        && sa.Address.IsEnabled)
                    .FirstOrDefaultAsync();

            if (foundAddress == null) return true;

            foundAddress.Address.IsEnabled = false;
            foundAddress.Address.LastModifiedDate = DateTime.UtcNow;
            foundAddress.IsEnabled = false;
            foundAddress.LastModifiedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}