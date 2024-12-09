using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Unit
    {
        public Unit()
        {
            FilamentMaterials = new HashSet<FilamentMaterial>();
            Filaments = new HashSet<Filament>();
            ModelFiles = new HashSet<ModelFile>();
            SellerProductVariants = new HashSet<SellerProductVariant>();
            StoreProductVariants = new HashSet<StoreProductVariant>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual UnitCategory? Category { get; set; }
        public virtual ICollection<FilamentMaterial> FilamentMaterials { get; set; }
        public virtual ICollection<Filament> Filaments { get; set; }
        public virtual ICollection<ModelFile> ModelFiles { get; set; }
        public virtual ICollection<SellerProductVariant> SellerProductVariants { get; set; }
        public virtual ICollection<StoreProductVariant> StoreProductVariants { get; set; }
    }
}
