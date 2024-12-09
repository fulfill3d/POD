using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            StoreSaleOrderStatuses = new HashSet<StoreSaleOrderStatus>();
        }

        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<StoreSaleOrderStatus> StoreSaleOrderStatuses { get; set; }
    }
}
