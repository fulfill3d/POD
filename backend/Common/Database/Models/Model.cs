using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Model
    {
        public Model()
        {
            ModelFiles = new HashSet<ModelFile>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Summary { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsEnabled { get; set; }
        public int ModelCategoryId { get; set; }

        public virtual ModelCategory ModelCategory { get; set; } = null!;
        public virtual ICollection<ModelFile> ModelFiles { get; set; }
    }
}
