using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Models;

namespace TukanTomek.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .HasOne(u => u.Family)
                    .WithMany(f => f.Users)
                    .HasForeignKey(u => u.FamilyId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Family>()
               .HasMany(f => f.Users)
               .WithOne(u => u.Family)
               .HasForeignKey(u => u.FamilyId)
               .IsRequired(false);
        }
    }
}
