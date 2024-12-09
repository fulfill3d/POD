using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleOrderAddress
    {
        public StoreSaleOrderAddress()
        {
            StoreSaleOrders = new HashSet<StoreSaleOrder>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string? CountryCode { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string Province { get; set; } = null!;
        public string? ProvinceCode { get; set; }
        public string Zip { get; set; } = null!;

        public virtual ICollection<StoreSaleOrder> StoreSaleOrders { get; set; }
    }
}
