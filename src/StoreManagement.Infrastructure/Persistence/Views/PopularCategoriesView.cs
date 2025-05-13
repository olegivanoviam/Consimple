namespace StoreManagement.Infrastructure.Persistence.Views;

public class PopularCategoriesView
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int TotalUnits { get; set; }
} 