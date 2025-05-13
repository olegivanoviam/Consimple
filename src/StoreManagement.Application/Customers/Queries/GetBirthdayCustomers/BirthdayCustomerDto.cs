namespace StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;

public class BirthdayCustomerDto
{
    public BirthdayCustomerDto(int id, string fullName)
    {
        Id = id;
        FullName = fullName;
    }

    public int Id { get; set; }
    public string FullName { get; set; }
}