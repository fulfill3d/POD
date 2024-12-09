using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class Configuration
    {
        public int ConfigurationId { get; set; }
        public string Name { get; set; } = null!;
        public int Type { get; set; }
        public string Configuration1 { get; set; } = null!;
    }
}
