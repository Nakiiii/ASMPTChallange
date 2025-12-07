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

            // Configure one-to-many relationship: Order -> Boards
            modelBuilder.Entity<Board>()
                .HasOne(b => b.Order)
                .WithMany(o => o.Boards)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure one-to-many relationship: Board -> Components
            modelBuilder.Entity<Component>()
                .HasOne(c => c.Board)
                .WithMany(b => b.Components)
                .HasForeignKey(c => c.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

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
