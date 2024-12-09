using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Seller
    {
        public Seller()
        {
            SellerAddresses = new HashSet<SellerAddress>();
            SellerPaymentMethods = new HashSet<SellerPaymentMethod>();
            SellerProducts = new HashSet<SellerProduct>();
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public decimal? Discount { get; set; }
        public bool? HasBeenUpdated { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? Status { get; set; }
        public int UserId { get; set; }
        public Guid UserRefId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<SellerAddress> SellerAddresses { get; set; }
        public virtual ICollection<SellerPaymentMethod> SellerPaymentMethods { get; set; }
        public virtual ICollection<SellerProduct> SellerProducts { get; set; }
        public virtual ICollection<Store> Stores { get; set; }
    }
}
