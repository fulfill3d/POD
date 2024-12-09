using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Address
    {
        public Address()
        {
            SellerAddresses = new HashSet<SellerAddress>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street1 { get; set; } = null!;
        public string? Street2 { get; set; }
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<SellerAddress> SellerAddresses { get; set; }
    }
}
