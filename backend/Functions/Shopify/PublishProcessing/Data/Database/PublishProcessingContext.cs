using Microsoft.EntityFrameworkCore;

namespace POD.Functions.Shopify.PublishProcessing.Data.Database
{
    public partial class PublishProcessingContext(DbContextOptions<PublishProcessingContext> options)
        : DbContext(options)
    {
        public virtual DbSet<POD.Common.Database.Models.Store> Stores { get; set; }
        public virtual DbSet<POD.Common.Database.Models.Seller> Sellers { get; set; }
        public virtual DbSet<POD.Common.Database.Models.StoreProduct> StoreProducts { get; set; }
        public virtual DbSet<POD.Common.Database.Models.StoreProductVariant> StoreProductVariants { get; set; }
        public virtual DbSet<POD.Common.Database.Models.StoreProductVariantImage> StoreProductVariantImages { get; set; }
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