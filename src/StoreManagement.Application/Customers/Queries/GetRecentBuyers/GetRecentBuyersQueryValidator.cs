using FluentValidation;

namespace StoreManagement.Application.Customers.Queries.GetRecentBuyers;

public class GetRecentBuyersQueryValidator : AbstractValidator<GetRecentBuyersQuery>
{
    public GetRecentBuyersQueryValidator()
    {
        RuleFor(x => x.Days)
            .GreaterThan(0)
            .WithMessage("Days must be greater than 0")
            .LessThanOrEqualTo(365)
            .WithMessage("Days cannot exceed 365");
    }
} 