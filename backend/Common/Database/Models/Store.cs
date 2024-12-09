using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Store
    {
        public Store()
        {
            StoreProducts = new HashSet<StoreProduct>();
            StoreSaleOrders = new HashSet<StoreSaleOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string? RefreshToken { get; set; }
        public DateTime? TokenExpireDate { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime? LastSyncDate { get; set; }
        public string ShopIdentifier { get; set; } = null!;
        public bool IsTokenRevoked { get; set; }
        public int MarketPlaceId { get; set; }
        public int SellerId { get; set; }
        public bool? IsShopifyScopeUpdated { get; set; }
        public string? Status { get; set; }
        public Guid UserRefId { get; set; }

        public virtual MarketPlace MarketPlace { get; set; } = null!;
        public virtual Seller Seller { get; set; } = null!;
        public virtual ICollection<StoreProduct> StoreProducts { get; set; }
        public virtual ICollection<StoreSaleOrder> StoreSaleOrders { get; set; }
    }
}
