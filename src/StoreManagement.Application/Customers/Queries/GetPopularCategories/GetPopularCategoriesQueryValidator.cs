using FluentValidation;

namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

public class GetPopularCategoriesQueryValidator : AbstractValidator<GetPopularCategoriesQuery>
{
    public GetPopularCategoriesQueryValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("CustomerId must be greater than 0");
    }
} 