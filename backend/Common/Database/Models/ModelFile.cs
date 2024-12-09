using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class ModelFile
    {
        public ModelFile()
        {
            ProductPieces = new HashSet<ProductPiece>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ThreeDmodelId { get; set; }
        public string Uri { get; set; } = null!;
        public string Type { get; set; } = null!;
        public long Size { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string BlobName { get; set; } = null!;
        public bool IsVolumeDetermined { get; set; }
        public decimal? Volume { get; set; }
        public int VolumeUnitId { get; set; }
        public bool IsBoundingBoxDetermined { get; set; }
        public decimal? BoundX { get; set; }
        public decimal? BoundY { get; set; }
        public decimal? BoundZ { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual Model ThreeDmodel { get; set; } = null!;
        public virtual Unit VolumeUnit { get; set; } = null!;
        public virtual ICollection<ProductPiece> ProductPieces { get; set; }
    }
}
