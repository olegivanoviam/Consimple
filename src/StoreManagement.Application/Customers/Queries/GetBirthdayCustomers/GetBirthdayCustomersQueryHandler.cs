using MediatR;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Common.Interfaces;

namespace StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;

public class GetBirthdayCustomersQueryHandler : IRequestHandler<GetBirthdayCustomersQuery, IEnumerable<BirthdayCustomerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetBirthdayCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BirthdayCustomerDto>> Handle(GetBirthdayCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(c => c.DateOfBirth.Month == request.Date.Month && c.DateOfBirth.Day == request.Date.Day)
            .Select(c => new BirthdayCustomerDto(c.Id, c.FullName))
            .ToListAsync(cancellationToken);
    }
}