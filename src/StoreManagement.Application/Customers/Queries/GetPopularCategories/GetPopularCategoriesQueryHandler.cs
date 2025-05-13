using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Common.Interfaces;

namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

public class GetPopularCategoriesQueryHandler : IRequestHandler<GetPopularCategoriesQuery, IEnumerable<CategoryPurchaseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPopularCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryPurchaseDto>> Handle(GetPopularCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Purchases
            .Where(p => p.CustomerId == request.CustomerId)
            .SelectMany(p => p.Items)
            .GroupBy(pi => pi.Product.Category)
            .Select(g => new CategoryPurchaseDto(
                g.Key.Id,
                g.Key.Name,
                g.Sum(pi => pi.Quantity)))
            .ToListAsync(cancellationToken);
    }
}