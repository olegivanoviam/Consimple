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
        // First verify if customer exists
        var customerExists = await _context.Customers
            .AnyAsync(c => c.Id == request.CustomerId, cancellationToken);

        if (!customerExists)
        {
            throw new InvalidOperationException($"Customer with ID {request.CustomerId} not found.");
        }

        return await _context.Set<PopularCategoryView>()
            .FromSqlInterpolated($@"
                WITH CustomerPurchases AS (
                    SELECT 
                        pc.Id AS CategoryId,
                        pc.Name AS CategoryName,
                        COALESCE(SUM(pi.Quantity), 0) AS TotalUnits
                    FROM ProductCategories pc
                    LEFT JOIN Products p ON p.CategoryId = pc.Id
                    LEFT JOIN PurchaseItems pi ON pi.ProductId = p.Id
                    LEFT JOIN Purchases pur ON pur.Id = pi.PurchaseId 
                        AND pur.CustomerId = {request.CustomerId}
                    GROUP BY pc.Id, pc.Name
                )
                SELECT 
                    CategoryId,
                    CategoryName,
                    TotalUnits
                FROM CustomerPurchases
                WHERE TotalUnits > 0
                ORDER BY TotalUnits DESC")
            .AsNoTracking()
            .Select(v => new CategoryPurchaseDto(
                v.CategoryId,
                v.CategoryName,
                v.TotalUnits))
            .ToListAsync(cancellationToken);
    }
}