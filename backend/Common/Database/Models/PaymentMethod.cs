using System;
using System.Collections.Generic;

namespace POD.Common.Database.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            SellerPaymentMethods = new HashSet<SellerPaymentMethod>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<SellerPaymentMethod> SellerPaymentMethods { get; set; }
    }
}
