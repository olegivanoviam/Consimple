namespace StoreManagement.Domain.Entities;

public class PurchaseItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int PurchaseId { get; set; }
    public Purchase Purchase { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}