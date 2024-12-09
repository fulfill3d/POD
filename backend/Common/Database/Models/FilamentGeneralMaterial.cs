using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class FilamentGeneralMaterial
    {
        public FilamentGeneralMaterial()
        {
            FilamentMaterials = new HashSet<FilamentMaterial>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NozzleTemperature { get; set; }
        public string? BedTemperature { get; set; }
        public string? HeatBed { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<FilamentMaterial> FilamentMaterials { get; set; }
    }
}
