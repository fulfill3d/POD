using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.Functions.Geometry.Data.Database
{
    public partial class GeometryContext(DbContextOptions<GeometryContext> options) : DbContext(options)
    {
        
        public virtual DbSet<ModelFile> ModelFiles { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            // //If the microservice has own context but separate db, add its config to the context
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