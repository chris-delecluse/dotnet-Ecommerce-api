using System;
using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DataAccess;

public class DatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<ProductQuantities> ProductQuantities { get; set; }

    private readonly IConfiguration _configuration;

    public DatabaseContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionStrings = _configuration.GetValue<string>("ConnectionStrings:Mysql");

        optionsBuilder.UseMySQL(connectionStrings!);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
            entity.HasIndex(e => e.Email).IsUnique());

        modelBuilder.Entity<Product>(entity =>
            entity.HasOne<ProductQuantities>(p => p.Quantities)
                .WithOne(pq => pq.Product)
                .HasForeignKey<ProductQuantities>(pq => pq.Id)
        );

        base.OnModelCreating(modelBuilder);
    }
}