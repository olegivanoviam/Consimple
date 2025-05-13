namespace StoreManagement.Domain.Entities;

public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public DateTime RegistrationDate { get; set; }
    public ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}