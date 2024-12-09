using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class UnitCategory
    {
        public UnitCategory()
        {
            Units = new HashSet<Unit>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Unit> Units { get; set; }
    }
}
