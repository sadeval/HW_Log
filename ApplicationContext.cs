using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Models;
using System;

namespace Shop
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Shop;Trusted_Connection=True;TrustServerCertificate=True;");

          
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole())) 
                .EnableSensitiveDataLogging() 
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Products)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 1200m },
                new Product { Id = 2, Name = "Mouse", Price = 25m }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = DateTime.Now }
            );
        }
    }
}
