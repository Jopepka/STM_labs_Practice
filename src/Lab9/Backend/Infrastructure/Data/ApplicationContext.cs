using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyPhone> CompanyPhones { get; set; }
    public DbSet<CompanyCategory> CompanyCategories { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Building>().HasData(
            new Building { Id = 1, Address = "Bluchera 32/1", Latitude = 55.751244, Longitude = 37.618423 },
            new Building { Id = 2, Address = "Tverskaya 7", Latitude = 55.76522, Longitude = 37.60556 }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Food" },
            new Category { Id = 2, CategoryName = "Semi-finished products wholesale", ParentCategoryId = 1 },
            new Category { Id = 3, CategoryName = "Meat products", ParentCategoryId = 1 },
            new Category { Id = 4, CategoryName = "Automobiles" },
            new Category { Id = 5, CategoryName = "Lada", ParentCategoryId = 4 },
            new Category { Id = 6, CategoryName = "Lada Granta", ParentCategoryId = 5 },
            new Category { Id = 7, CategoryName = "Lada Largus", ParentCategoryId = 5 },
            new Category { Id = 8, CategoryName = "Audi", ParentCategoryId = 4 }
        );

        modelBuilder.Entity<Company>().HasData(
            new Company { Id = 1, CompanyName = "ООО Рога и Копыта", BuildingId = 1 },
            new Company { Id = 2, CompanyName = "ООО Машины и Механизмы", BuildingId = 2 }
        );

        modelBuilder.Entity<CompanyCategory>().HasData(
            new CompanyCategory { Id = 1, CompanyId = 1, CategoryId = 2 },
            new CompanyCategory { Id = 2, CompanyId = 1, CategoryId = 3 },
            new CompanyCategory { Id = 3, CompanyId = 2, CategoryId = 4 },
            new CompanyCategory { Id = 4, CompanyId = 2, CategoryId = 5 },
            new CompanyCategory { Id = 5, CompanyId = 2, CategoryId = 6 }
        );

        modelBuilder.Entity<CompanyPhone>().HasData(
            new CompanyPhone { Id = 1, CompanyId = 1, Phone = "2-222-222" },
            new CompanyPhone { Id = 2, CompanyId = 1, Phone = "3-333-333" },
            new CompanyPhone { Id = 3, CompanyId = 1, Phone = "8-923-666-13-13" },
            new CompanyPhone { Id = 4, CompanyId = 2, Phone = "4-444-444" }
        );
    }
}
