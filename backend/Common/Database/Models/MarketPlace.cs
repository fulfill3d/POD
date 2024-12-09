using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class MarketPlace
    {
        public MarketPlace()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
