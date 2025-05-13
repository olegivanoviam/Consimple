using Microsoft.EntityFrameworkCore;

namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

[Keyless]
public class PopularCategoryView
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int TotalUnits { get; set; }
} 