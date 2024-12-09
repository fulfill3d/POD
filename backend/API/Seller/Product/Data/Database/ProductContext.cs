using Microsoft.EntityFrameworkCore;

namespace POD.API.Seller.Product.Data.Database
{
    public partial class ProductContext(DbContextOptions<ProductContext> options) : DbContext(options)
    {
        public virtual DbSet<Common.Database.Models.SellerProduct> SellerProducts { get; set; }
        public virtual DbSet<Common.Database.Models.SellerProductVariant> SellerProductVariants { get; set; }
        public virtual DbSet<Common.Database.Models.SellerProductVariantImage> SellerProductVariantImages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Common.Database.Models.DatabaseContext).Assembly);

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