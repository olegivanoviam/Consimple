using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class PopularCategoriesViewConfiguration : IEntityTypeConfiguration<PopularCategoriesView>
{
    public void Configure(EntityTypeBuilder<PopularCategoriesView> builder)
    {
        builder.HasNoKey();

        builder.Property(e => e.CategoryId);
        builder.Property(e => e.CategoryName);
        builder.Property(e => e.TotalUnits);

        builder.ToView(null);
    }
} 