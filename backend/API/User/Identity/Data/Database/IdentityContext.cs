using Microsoft.EntityFrameworkCore;

namespace POD.API.User.Identity.Data.Database
{
    public partial class IdentityContext(DbContextOptions<IdentityContext> options) : DbContext(options)
    {
        
        public virtual DbSet<POD.Common.Database.Models.User> Users { get; set; }
        public virtual DbSet<POD.Common.Database.Models.Seller> Sellers { get; set; }
        
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