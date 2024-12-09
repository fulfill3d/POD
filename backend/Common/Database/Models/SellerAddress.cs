using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class SellerAddress
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public int SellerId { get; set; }
        public int AddressId { get; set; }
        public Guid UserRefId { get; set; }

        public virtual Address Address { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
    }
}
