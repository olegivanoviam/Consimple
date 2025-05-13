namespace StoreManagement.Application.Customers.Queries.GetRecentBuyers;

public class RecentBuyerDto
{
    public RecentBuyerDto(int id, string fullName, DateTime lastPurchaseDate)
    {
        Id = id;
        FullName = fullName;
        LastPurchaseDate = lastPurchaseDate;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
    public DateTime LastPurchaseDate { get; set; }
}