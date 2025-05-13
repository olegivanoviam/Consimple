namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

public class CategoryPurchaseDto
{
    public CategoryPurchaseDto(int categoryId, string categoryName, int totalUnits)
    {
        CategoryId = categoryId;
        CategoryName = categoryName;
        TotalUnits = totalUnits;
    }

    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public int TotalUnits { get; set; }
}