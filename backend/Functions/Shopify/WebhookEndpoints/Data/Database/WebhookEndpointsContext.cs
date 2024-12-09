using Microsoft.EntityFrameworkCore;

namespace POD.Functions.Shopify.WebhookEndpoints.Data.Database
{
    public partial class WebhookEndpointsContext(DbContextOptions<WebhookEndpointsContext> options) : DbContext(options)
    {
        public virtual DbSet<POD.Common.Database.Models.Store> Stores { get; set; }
        public virtual DbSet<POD.Common.Database.Models.Address> Addresses { get; set; }
        public virtual DbSet<POD.Common.Database.Models.SellerAddress> SellerAddresses { get; set; }
        public virtual DbSet<POD.Common.Database.Models.BlockedUser> BlockedUser { get; set; }
        public virtual DbSet<POD.Common.Database.Models.Seller> Sellers { get; set; }
        public virtual DbSet<POD.Common.Database.Models.User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(POD.Common.Database.Models.DatabaseContext).Assembly);

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