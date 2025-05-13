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
        var cutoffDate = DateTime.UtcNow.AddDays(-request.Days);

        return await _context.Customers
            .Where(c => c.Purchases.Any(p => p.Date >= cutoffDate))
            .Select(c => new RecentBuyerDto(
                c.Id,
                c.FullName,
                c.Purchases.Where(p => p.Date >= cutoffDate)
                    .Max(p => p.Date)))
            .ToListAsync(cancellationToken);
    }
}