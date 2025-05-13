using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.Entities;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.Property(e => e.Number).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.TotalAmount).HasPrecision(18, 2).IsRequired();

        builder.HasOne(e => e.Customer)
            .WithMany(e => e.Purchases)
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}