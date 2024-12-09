using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class ShadeColor
    {
        public ShadeColor()
        {
            Filaments = new HashSet<Filament>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public int? GeneralColorId { get; set; }
        public bool? IsActive { get; set; }

        public virtual GeneralColor? GeneralColor { get; set; }
        public virtual ICollection<Filament> Filaments { get; set; }
    }
}
