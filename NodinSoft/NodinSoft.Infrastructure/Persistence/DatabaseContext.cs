using NodinSoft.Domain.Authentication; // Corrected
using NodinSoft.Domain.ProductManagement; // Corrected
using Microsoft.EntityFrameworkCore;

namespace NodinSoft.Infrastructure.Persistence // Corrected namespace
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
            // The relationship is now primarily configured from the User side (User.Products)
            // and through the Product.UserId foreign key.
            // We still need to ensure the FK is correctly set up if not by convention.
            // However, EF Core typically infers this if UserId property exists and User.Products collection exists.
            // Explicitly setting the FK if needed:
            // modelBuilder.Entity<Product>()
            //             .HasOne<User>() // Specify User type explicitly as there's no nav property
            //             .WithMany(u => u.Products)
            //             .HasForeignKey(p => p.UserId)
            //             .OnDelete(DeleteBehavior.Cascade);
            // For now, let's rely on the convention picked up from User.cs and Product.UserId
            // and remove the line causing the compile error.
            // If cascading delete is not picked by convention, it can be added to UserConfig.

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
                        .HasIndex(u => u.Email)
                        .IsUnique();

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