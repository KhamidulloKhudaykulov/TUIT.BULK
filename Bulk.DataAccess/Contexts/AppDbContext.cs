using Bulk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bulk.DataAccess.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>().HasData(
            new Actor { Id = 1, Name = "Xamidullo", Surname = "Xudaykulov", Email = "xudaykulov123@gmail.com", Password = "test1234", Phone = "+998900320802", Role = "SuperAdmin" });

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Sweets" });

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Crafers", Description = "", CategoryId = 1, Count = 100 });
    }
}
