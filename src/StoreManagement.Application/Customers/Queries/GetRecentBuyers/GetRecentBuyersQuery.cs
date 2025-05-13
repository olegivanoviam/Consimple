using MediatR;

namespace StoreManagement.Application.Customers.Queries.GetRecentBuyers;

public class GetRecentBuyersQuery : IRequest<IEnumerable<RecentBuyerDto>>
{
    public GetRecentBuyersQuery(int days)
    {
        Days = days;
    }

    public int Days { get; set; }
}