using MediatR;

namespace StoreManagement.Application.Customers.Queries.GetPopularCategories;

public class GetPopularCategoriesQuery : IRequest<IEnumerable<CategoryPurchaseDto>>
{
    public GetPopularCategoriesQuery(int customerId)
    {
        CustomerId = customerId;
    }

    public int CustomerId { get; set; }
}