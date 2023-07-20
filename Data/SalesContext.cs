using Microsoft.EntityFrameworkCore;

namespace WebApiEntityFrameworkDockerSqlServer.Data
{
    public class SalesContext : DbContext
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,4)");
        }
        public DbSet<Product>? Products { get; set; }
    }
}