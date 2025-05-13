using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class PopularCategoryViewConfiguration : IEntityTypeConfiguration<PopularCategoryView>
{
    public void Configure(EntityTypeBuilder<PopularCategoryView> builder)
    {
        builder.HasNoKey();
        builder.ToView(null);
    }
} 