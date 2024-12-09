using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class BlockedUser
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Shop { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
