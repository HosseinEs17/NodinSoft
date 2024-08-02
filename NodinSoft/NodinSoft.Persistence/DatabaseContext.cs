using NodinSoft.Entities.Authentication;
using NodinSoft.Entities.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace NodinSoft.Persistance
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ProductConfig(modelBuilder);
            UserConfig(modelBuilder);
        }

        public void ProductConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                        .HasOne(p => p.User)
                        .WithMany(u => u.Products)
                        .HasForeignKey(p => p.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                        .HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
                        .IsUnique();

            modelBuilder.Entity<Product>()
                        .Property(p => p.ProduceDate)
                        .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Product>()
                        .Property(p => p.IsAvailable)
                        .HasDefaultValue(true);

            modelBuilder.Entity<Product>()
                        .Property(p => p.ManufactureEmail)
                        .HasColumnType("varchar")
                        .HasMaxLength(64);

            modelBuilder.Entity<Product>()
                        .Property(p => p.ManufacturePhone)
                        .HasColumnType("varchar")
                        .HasMaxLength(16);

            modelBuilder.Entity<Product>()
                        .Property(p => p.Name)
                        .HasColumnType("nvarchar")
                        .HasMaxLength(64);
        }
        public void UserConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .Property(u => u.Email)
                        .HasColumnType("varchar")
                        .HasMaxLength(64);

            modelBuilder.Entity<User>()
                        .Property(u => u.PhoneNumber)
                        .HasColumnType("varchar")
                        .HasMaxLength(16);

            modelBuilder.Entity<User>()
                        .Property(u => u.Password)
                        .HasColumnType("nvarchar")
                        .HasMaxLength(64);
        }
    }
}