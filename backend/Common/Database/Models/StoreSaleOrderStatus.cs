using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleOrderStatus
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ProcessCount { get; set; }
        public int OrderStatusId { get; set; }
        public int StoreSaleOrderId { get; set; }

        public virtual OrderStatus OrderStatus { get; set; } = null!;
        public virtual StoreSaleOrder StoreSaleOrder { get; set; } = null!;
    }
}
