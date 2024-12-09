using Microsoft.EntityFrameworkCore;

namespace POD.API.Seller.Common.Data
{
    public partial class SellerCommonContext(DbContextOptions<SellerCommonContext> options) : DbContext(options)
    {
        public virtual DbSet<POD.Common.Database.Models.Seller> Sellers { get; set; }
        public virtual DbSet<POD.Common.Database.Models.Store> Stores { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            // //If the microservice has own context but separate db, add its config to the context
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(POD.Common.Database.Models.DatabaseContext).Assembly);

            // //If the microservice has own context and db, add its config to the context

            // base.OnModelCreating(modelBuilder);
            //
            // modelBuilder.Entity<Entity>(entity =>
            // {
            //     entity.HasKey(e => e.Id);
            //
            //     entity.Property(e => e.Id)
            //         .ValueGeneratedOnAdd();
            // });

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