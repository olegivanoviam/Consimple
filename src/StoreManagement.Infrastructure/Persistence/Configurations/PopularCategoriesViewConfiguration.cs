using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.Infrastructure.Persistence.Configurations;

public class PopularCategoriesViewConfiguration : IEntityTypeConfiguration<PopularCategoriesView>
{
    public void Configure(EntityTypeBuilder<PopularCategoriesView> builder)
    {
        builder.HasNoKey();
        builder.ToView(null);
    }
} 