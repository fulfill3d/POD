using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class User
    {
        public User()
        {
            Sellers = new HashSet<Seller>();
        }

        public int Id { get; set; }
        public Guid RefId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsEnabled { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool HasTakenTour { get; set; }
        public bool IsPrivacyPolicyAccepted { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Seller> Sellers { get; set; }
    }
}
