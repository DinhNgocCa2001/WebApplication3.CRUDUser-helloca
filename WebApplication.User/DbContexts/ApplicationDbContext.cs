using Microsoft.EntityFrameworkCore;
using WebApplication.CRUDUser.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.AccessControl;
using WebApplication.CRUDUser.Constants;
using System;

namespace WebApplication.CRUDUser.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<user> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        //public DbSet<CustomerUser> CustomerUsers { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.HasKey(c => c.CustomerId);
                entity.Property(c => c.CustomerId)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                entity.Property(c => c.FullName)
                   .IsUnicode()
                   .HasMaxLength(50)
                   .IsRequired();
                entity.Property(c => c.CCCD)
                .HasMaxLength(50)
                    .IsRequired();
                entity.Property(c => c.Address)
                    .IsUnicode()
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(c => c.DateOfBirth)
                    .HasColumnType("date")
                    .IsRequired();
            });

            modelBuilder.Entity<user>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserId)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
                entity.Property(u => u.Password)
                    .IsUnicode()
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(u => u.Email)
                    .IsUnicode()
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(u => u.Phone)
                    .IsRequired();
                entity.Property(u => u.UserType)
                    .HasDefaultValue(UserTypes.Customer)
                    .IsRequired();
                entity.Property(u => u.Username)
                    .IsUnicode()
                    .HasMaxLength(50)
                    .IsRequired();
                entity.Property(u => u.Status)
                    .HasDefaultValue(StatusTypes.Open)
                    .IsRequired();
            });

            
        }
    }
}
