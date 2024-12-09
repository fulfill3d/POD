using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.Functions.Payment.PostProcessing.Data.Database
{
    public partial class PostProcessingContext(DbContextOptions<PostProcessingContext> options) : DbContext(options)
    {
        public virtual DbSet<SellerPaymentTransaction> SellerPaymentTransactions { get; set; }
        public virtual DbSet<StoreSaleTransaction> StoreSaleTransactions { get; set; }
        public virtual DbSet<StoreSaleTransactionDetail> StoreSaleTransactionDetails { get; set; }
        public virtual DbSet<BraintreeTransactionDetail> BraintreeTransactionDetails { get; set; }
        public virtual DbSet<PayPalTransactionDetail> PayPalTransactionDetails { get; set; }
        public virtual DbSet<StripeTransactionDetail> StripeTransactionDetails { get; set; }
        
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