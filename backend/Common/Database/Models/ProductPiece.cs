using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class ProductPiece
    {
        public int Id { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int FilamentId { get; set; }
        public int ThreeDmodelFileId { get; set; }
        public int SellerProductVariantId { get; set; }

        public virtual Filament Filament { get; set; } = null!;
        public virtual SellerProductVariant SellerProductVariant { get; set; } = null!;
        public virtual ModelFile ThreeDmodelFile { get; set; } = null!;
    }
}
