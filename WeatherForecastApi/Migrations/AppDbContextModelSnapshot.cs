﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherForecastApi.WeatherForecastApi.Infrastructure.Data;

#nullable disable

namespace WeatherForecastApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WeatherForecastApi.WeatherForecastApi.Domain.Entities.FavoriteCity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("Name", "UserId")
                        .IsUnique()
                        .HasDatabaseName("IX_FavoriteCity_Name_UserId");

                    b.ToTable("FavoriteCities");
                });

            modelBuilder.Entity("WeatherForecastApi.WeatherForecastApi.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasDatabaseName("IX_User_Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WeatherForecastApi.WeatherForecastApi.Domain.Entities.FavoriteCity", b =>
                {
                    b.HasOne("WeatherForecastApi.WeatherForecastApi.Domain.Entities.User", null)
                        .WithMany("FavoriteCities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeatherForecastApi.WeatherForecastApi.Domain.Entities.User", b =>
                {
                    b.Navigation("FavoriteCities");
                });
#pragma warning restore 612, 618
        }
    }
}
