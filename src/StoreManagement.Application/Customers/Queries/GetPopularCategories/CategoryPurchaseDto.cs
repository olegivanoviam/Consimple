namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

public class CategoryPurchaseDto
{
    public CategoryPurchaseDto(string category, int totalUnits)
    {
        Category = category;
        TotalUnits = totalUnits;
    }

    public string Category { get; set; }
    public int TotalUnits { get; set; }
}