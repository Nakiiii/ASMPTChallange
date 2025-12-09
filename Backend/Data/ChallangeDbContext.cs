using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ChallangeDbContext : DbContext
    {
        public ChallangeDbContext(DbContextOptions<ChallangeDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Component> Components { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Many-to-Many for Order <-> Board
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Boards)
                .WithMany(b => b.Orders);

            // Configure Many-to-Many for Board <-> Component
            modelBuilder.Entity<Board>()
                .HasMany(b => b.Components)
                .WithMany(c => c.Boards);

            // Configure Description constraints to limit length
            modelBuilder.Entity<Order>()
                .Property(o => o.Description)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.Description)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Component>()
                .Property(c => c.Description)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
