namespace StoreManagement.Domain.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();
}