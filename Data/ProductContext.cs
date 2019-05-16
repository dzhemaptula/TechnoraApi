using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechnoraApi.Models;
using System;

namespace TechnoraApi.Data
{
    public class ProductContext : IdentityDbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .HasMany(p => p.Specifications)
                .WithOne()
                .IsRequired()
                .HasForeignKey("ProductId"); //Shadow property
            builder.Entity<Product>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(r => r.Price).IsRequired();

            builder.Entity<Specification>().Property(r => r.Description).IsRequired().HasMaxLength(500);
            builder.Entity<Specification>().Property(r => r.Type).HasDefaultValue(null);

            builder.Entity<Customer>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().Ignore(c => c.ItemsForSale);

            builder.Entity<CustomerItemsForSale>().HasKey(f => new { f.CustomerId, f.ProductId });
            builder.Entity<CustomerItemsForSale>().HasOne(f => f.Customer).WithMany(u => u.Items).HasForeignKey(f => f.CustomerId);
            builder.Entity<CustomerItemsForSale>().HasOne(f => f.Product).WithMany().HasForeignKey(f => f.ProductId);

            //Another way to seed the database
            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "iPhone", Created = DateTime.Now, Price=450 },
                new Product { Id = 2, Name = "Laptop", Created = DateTime.Now, Price=734 }
  );

            builder.Entity<Specification>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new { Id = 1, Description = "64GB", ProductId = 1, Type = "Positive"},
                    new { Id = 2, Description = "White", ProductId = 1, Type="Negative"},
                    new { Id = 3, Description = "6 Inch", ProductId = 1 },
                    new { Id = 4, Description = "Acer merk", ProductId = 2, Type = "Negative"}
                 );
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}