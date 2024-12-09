using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleOrder
    {
        public StoreSaleOrder()
        {
            OrderNotes = new HashSet<OrderNote>();
            StoreSaleOrderDetails = new HashSet<StoreSaleOrderDetail>();
            StoreSaleOrderStatuses = new HashSet<StoreSaleOrderStatus>();
        }

        public int Id { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? ShippingLabelId { get; set; }
        public decimal TotalCost { get; set; }
        public string? OrderNumber { get; set; }
        public string? StoreOrderIdentifier { get; set; }
        public string? StoreOrderNumber { get; set; }
        public string? TrackingNumber { get; set; }
        public string? ContactEmail { get; set; }
        public int StoreId { get; set; }
        public int StoreSaleOrderPriorityId { get; set; }
        public int StoreSaleOrderAddressId { get; set; }

        public virtual Store Store { get; set; } = null!;
        public virtual StoreSaleOrderAddress StoreSaleOrderAddress { get; set; } = null!;
        public virtual StoreSaleOrderPriority StoreSaleOrderPriority { get; set; } = null!;
        public virtual ICollection<OrderNote> OrderNotes { get; set; }
        public virtual ICollection<StoreSaleOrderDetail> StoreSaleOrderDetails { get; set; }
        public virtual ICollection<StoreSaleOrderStatus> StoreSaleOrderStatuses { get; set; }
    }
}
