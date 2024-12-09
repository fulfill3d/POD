using POD.Common.Database.Models;
using POD.Functions.Shopify.WebhookEndpoints.Services.Interfaces;
using POD.Integrations.ShopifyClient.Model;

namespace POD.Functions.Shopify.WebhookEndpoints.Services
{
    public class AddressService : IAddressService
    {

        public AddressService()
        {
        }

        public async Task<SellerAddress> CreateAddress(ShopifySeller shopifySeller)
        {
            var address = new Address
            {
                FirstName = "Test", // TODO Address's first name does not accept null values but shopifySeller has null
                LastName = "Test", // TODO Address's first name does not accept null values but shopifySeller has null
                Street1 = shopifySeller.Address1 == null ? "": shopifySeller.Address1,
                Street2 = shopifySeller.Address2 == null ? "" : shopifySeller.Address2,
                City = shopifySeller.City == null ? "": shopifySeller.City,
                State = shopifySeller.ProvinceCode == null ? "" : shopifySeller.ProvinceCode.ToString(),
                Country = shopifySeller.CountryCode == null ? "" : shopifySeller.CountryCode,
                ZipCode = shopifySeller.Zip == null ? "" : shopifySeller.Zip,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow,
                UserId = 1
            };

            var customerAddress = new SellerAddress
            {
                Address = address,
                IsEnabled = true,
                LastModifiedDate = DateTime.UtcNow,
                UserRefId = Guid.NewGuid(), // TODO Address
            };

            return customerAddress;
        }
    }
}
