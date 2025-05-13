using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Name).HasMaxLength(200).IsRequired();
        builder.Property(e => e.Category).HasMaxLength(100).IsRequired();
        builder.Property(e => e.SKU).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Price).HasPrecision(18, 2).IsRequired();
    }
}