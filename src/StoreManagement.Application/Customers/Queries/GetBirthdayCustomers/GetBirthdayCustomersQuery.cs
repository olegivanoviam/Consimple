using MediatR;

namespace StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;

public class GetBirthdayCustomersQuery : IRequest<IEnumerable<BirthdayCustomerDto>>
{
    public GetBirthdayCustomersQuery(DateTime date)
    {
        Date = date;
    }

    public DateTime Date { get; set; }
}