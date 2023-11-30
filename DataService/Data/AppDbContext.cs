using Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace DataService.Data
{
    public class AppDbContext : DbContext
    {
        //Define the db entities
        public virtual DbSet<Developer> Developers { get; set; }
        public virtual DbSet<Achievement> Achievements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Specify the relationship between the entities
            modelBuilder.Entity<Achievement>(entity =>
            {
                entity.HasOne(d => d.Developer)
                      .WithMany(p => p.Achievements)
                      .HasForeignKey(d => d.DeveloperId)
                      .OnDelete(DeleteBehavior.NoAction)
                      .HasConstraintName("FK_Achivements_Driver");
            });
        }
    }
}
