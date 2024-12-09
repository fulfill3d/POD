using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class ModelCategory
    {
        public ModelCategory()
        {
            InverseParentNavigation = new HashSet<ModelCategory>();
            Models = new HashSet<Model>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Parent { get; set; }
        public bool? IsEnabled { get; set; }

        public virtual ModelCategory? ParentNavigation { get; set; }
        public virtual ICollection<ModelCategory> InverseParentNavigation { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
