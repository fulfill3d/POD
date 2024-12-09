using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class GeneralColor
    {
        public GeneralColor()
        {
            ShadeColors = new HashSet<ShadeColor>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Hex { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ShadeColor> ShadeColors { get; set; }
    }
}
