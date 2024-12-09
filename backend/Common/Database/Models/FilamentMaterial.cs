using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class FilamentMaterial
    {
        public FilamentMaterial()
        {
            Filaments = new HashSet<Filament>();
        }

        public int Id { get; set; }
        public int? GeneralMaterialId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public decimal Density { get; set; }
        public int DensityUnitId { get; set; }

        public virtual Unit DensityUnit { get; set; } = null!;
        public virtual FilamentGeneralMaterial? GeneralMaterial { get; set; }
        public virtual ICollection<Filament> Filaments { get; set; }
    }
}
