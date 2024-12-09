using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Filament
    {
        public Filament()
        {
            ProductPieces = new HashSet<ProductPiece>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; } = null!;
        public decimal Cost { get; set; }
        public int StockQuantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsActive { get; set; }
        public decimal SpoolWeight { get; set; }
        public int SpoolWeightUnitId { get; set; }

        public virtual FilamentBrand Brand { get; set; } = null!;
        public virtual ShadeColor Color { get; set; } = null!;
        public virtual FilamentMaterial Material { get; set; } = null!;
        public virtual Unit SpoolWeightUnit { get; set; } = null!;
        public virtual ICollection<ProductPiece> ProductPieces { get; set; }
    }
}
