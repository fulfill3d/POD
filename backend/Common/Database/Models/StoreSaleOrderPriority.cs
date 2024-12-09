using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class StoreSaleOrderPriority
    {
        public StoreSaleOrderPriority()
        {
            StoreSaleOrders = new HashSet<StoreSaleOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<StoreSaleOrder> StoreSaleOrders { get; set; }
    }
}
