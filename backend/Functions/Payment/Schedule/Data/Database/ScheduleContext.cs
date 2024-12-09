using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.Functions.Payment.Schedule.Data.Database
{
    public partial class ScheduleContext(DbContextOptions<ScheduleContext> options) : DbContext(options)
    {
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<SellerPaymentMethod> SellerPaymentMethods { get; set; }
        public virtual DbSet<PaypalDetail> PaypalDetails { get; set; }
        public virtual DbSet<BraintreeDetail> BraintreeDetails { get; set; }
        public virtual DbSet<StripeDetail> StripeDetails { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreSaleOrder> StoreSaleOrders { get; set; }
        public virtual DbSet<StoreSaleOrderDetail> StoreSaleOrderDetails { get; set; }
        public virtual DbSet<StoreSaleOrderStatus> StoreSaleOrderStatuses { get; set; }
        
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