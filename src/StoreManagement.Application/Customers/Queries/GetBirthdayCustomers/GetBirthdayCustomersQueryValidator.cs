using FluentValidation;

namespace StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;

public class GetBirthdayCustomersQueryValidator : AbstractValidator<GetBirthdayCustomersQuery>
{
    public GetBirthdayCustomersQueryValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .Must(date => date.Year >= 1900 && date.Year <= 2100)
            .WithMessage("Date must be between years 1900 and 2100");
    }
} 