using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<Purchase> Purchases { get; }
    DbSet<PurchaseItem> PurchaseItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}