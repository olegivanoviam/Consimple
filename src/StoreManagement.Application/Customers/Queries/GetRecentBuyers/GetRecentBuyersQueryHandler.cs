using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Common.Interfaces;

namespace StoreManagement.Application.Customers.Queries.GetRecentBuyers;

public class GetRecentBuyersQueryHandler : IRequestHandler<GetRecentBuyersQuery, IEnumerable<RecentBuyerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRecentBuyersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecentBuyerDto>> Handle(GetRecentBuyersQuery request, CancellationToken cancellationToken)
    {
        var cutoffDate = DateTime.UtcNow.Date.AddDays(-request.Days);

        return await _context.Set<RecentBuyerView>()
            .FromSqlInterpolated($@"
                SELECT 
                    c.Id AS CustomerId,
                    c.FullName,
                    MAX(p.Date) AS LastPurchaseDate
                FROM Customers c
                INNER JOIN Purchases p ON c.Id = p.CustomerId
                WHERE p.Date >= {cutoffDate}
                GROUP BY c.Id, c.FullName")
            .AsNoTracking()
            .Select(r => new RecentBuyerDto(
                r.CustomerId,
                r.FullName,
                r.LastPurchaseDate))
            .ToListAsync(cancellationToken);
    }
}