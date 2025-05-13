using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Common.Interfaces;
using StoreManagement.Domain.Entities;
using StoreManagement.Infrastructure.Persistence.Views;
using System.Reflection;

namespace StoreManagement.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Purchase> Purchases => Set<Purchase>();
    public DbSet<PurchaseItem> PurchaseItems => Set<PurchaseItem>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<PopularCategoriesView> PopularCategoriesView => Set<PopularCategoriesView>();

    public new DbSet<TEntity> Set<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<PopularCategoriesView>()
            .HasNoKey()
            .ToView(null);

        base.OnModelCreating(modelBuilder);
    }
}