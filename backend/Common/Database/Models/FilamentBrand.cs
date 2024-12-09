using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class FilamentBrand
    {
        public FilamentBrand()
        {
            Filaments = new HashSet<Filament>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Origin { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Filament> Filaments { get; set; }
    }
}
