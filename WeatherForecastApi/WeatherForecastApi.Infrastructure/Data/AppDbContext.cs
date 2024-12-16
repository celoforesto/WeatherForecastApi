using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherForecastApi.WeatherForecastApi.Domain.Entities;

namespace WeatherForecastApi.WeatherForecastApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<FavoriteCity> FavoriteCities { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FavoriteCityConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }

    public class FavoriteCityConfig : IEntityTypeConfiguration<FavoriteCity>
    {
        public void Configure(EntityTypeBuilder<FavoriteCity> builder)
        {
            builder.HasKey(fc => fc.Id);

            builder.Property(fc => fc.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            // Define unique index for Name and UserId
            builder.HasIndex(fc => new { fc.Name, fc.UserId })
                   .IsUnique()
                   .HasDatabaseName("IX_FavoriteCity_Name_UserId");

            builder.HasOne<User>()
                   .WithMany(u => u.FavoriteCities)
                   .HasForeignKey(fc => fc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            // Define unique index for Username
            builder.HasIndex(u => u.Username)
                   .IsUnique()
                   .HasDatabaseName("IX_User_Username");

            builder.Property(u => u.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(200);

            // Define relationship with FavoriteCities
            builder.HasMany(u => u.FavoriteCities)
                   .WithOne()
                   .HasForeignKey(fc => fc.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
