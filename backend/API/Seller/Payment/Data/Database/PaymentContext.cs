using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.API.Seller.Payment.Data.Database
{
    public partial class PaymentContext(DbContextOptions<PaymentContext> options) : DbContext(options)
    {
        
        public virtual DbSet<Common.Database.Models.Seller> Sellers { get; set; }
        public virtual DbSet<Common.Database.Models.SellerPaymentMethod> SellerPaymentMethods { get; set; }
        public virtual DbSet<Common.Database.Models.BraintreeDetail> BraintreeDetails { get; set; }
        public virtual DbSet<Common.Database.Models.StripeDetail> StripeDetails { get; set; }
        public virtual DbSet<Common.Database.Models.Store> Stores { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public void RevertAllChangesInTheContext()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}