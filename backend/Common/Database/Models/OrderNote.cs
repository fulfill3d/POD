using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class OrderNote
    {
        public int Id { get; set; }
        public string Note { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        public int StoreSaleOrderId { get; set; }

        public virtual StoreSaleOrder StoreSaleOrder { get; set; } = null!;
    }
}
