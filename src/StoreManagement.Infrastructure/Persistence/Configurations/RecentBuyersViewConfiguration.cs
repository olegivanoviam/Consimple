using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Application.Customers.Queries.GetRecentBuyers;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class RecentBuyersViewConfiguration : IEntityTypeConfiguration<RecentBuyersView>
{
    public void Configure(EntityTypeBuilder<RecentBuyersView> builder)
    {
        builder.HasNoKey();
        
        builder.Property(x => x.CustomerId);
        builder.Property(x => x.FullName);
        builder.Property(x => x.LastPurchaseDate);

        builder.ToView(null);
    }
} 