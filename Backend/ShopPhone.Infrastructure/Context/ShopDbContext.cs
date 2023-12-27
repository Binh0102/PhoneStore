using Microsoft.EntityFrameworkCore;
using ShopPhone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPhone.Infrastructure.Context
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext()
        {

        }
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) 
        {
            
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Orderdetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Category");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
            });
            modelBuilder.Entity<User>(e => {
                e.ToTable("User");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.username)
                    .IsRequired()
                     .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.status)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
            });
            modelBuilder.Entity<Product>(e => {
                e.ToTable("Product");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.ProductName) 
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("float");
                e.Property(e => e.MemoryStorage)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasColumnType("int");
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Order");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar");
                e.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("float");
                e.Property(e => e.Orderdate)
                    .IsRequired()
                    .HasColumnType("date");
                e.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType ("int");
                e.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType ("int");
            });
            modelBuilder.Entity<Orderdetail>(e =>
            {
                e.ToTable("Orderdetail");
                e.HasKey(e => e.Id);
                e.Property(e => e.Id).IsRequired();
                e.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("float");
                e.Property(e => e.Quantity)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.ProductId)
                    .IsRequired()
                    .HasColumnType("int");
                e.Property(e => e.OrderId)
                    .IsRequired()
                    .HasColumnType("int");
            });
        }
    }
}
