namespace StoreManagement.Application.Customers.Queries.GetRecentBuyers;

public class RecentBuyerView
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime LastPurchaseDate { get; set; }
} 