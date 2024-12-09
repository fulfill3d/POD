using Microsoft.EntityFrameworkCore;
using POD.Common.Database.Models;

namespace POD.Functions.PublishSchedule.Data.Database
{
    public partial class PublishScheduleContext(DbContextOptions<PublishScheduleContext> options) : DbContext(options)
    {
        
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoreProduct> StoreProducts { get; set; }
        
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