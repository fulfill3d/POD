using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.API.Admin.Filament.Data.Database
{
    public partial class FilamentContext(DbContextOptions<FilamentContext> options) : DbContext(options)
    {
        
        public virtual DbSet<Common.Database.Models.Filament> Filaments { get; set; }
        public virtual DbSet<Common.Database.Models.FilamentBrand> FilamentBrands { get; set; }
        public virtual DbSet<Common.Database.Models.ShadeColor> ShadeColors { get; set; }
        public virtual DbSet<Common.Database.Models.FilamentMaterial> FilamentMaterials { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            // //If the microservice has own context but separate db, add its config to the context
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

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