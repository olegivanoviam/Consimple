using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.IntegrationTests.Customers.Queries;

[TestFixture]
public class GetPopularCategoriesTests : TestBase
{
    private GetPopularCategoriesQueryHandler _handler;

    [OneTimeSetUp]
    public void SetUpTestData()
    {
        _handler = new GetPopularCategoriesQueryHandler(Context);
    }

    [Test]
    public async Task GetPopularCategories_ShouldReturnCorrectData()
    {
        // Arrange
        var customers = await Context.Customers.ToListAsync();

        foreach (var customer in customers)
        {
            // Get results from handler
            var query = new GetPopularCategoriesQuery(customer.Id);
            var handlerResult = await _handler.Handle(query, CancellationToken.None);

            // Get expected results using direct LINQ query
            var expectedCategories = await Context.ProductCategories
                .Select(pc => new
                {
                    CategoryId = pc.Id,
                    CategoryName = pc.Name,
                    TotalUnits = Context.PurchaseItems
                        .Where(pi => pi.Product.CategoryId == pc.Id &&
                                   pi.Purchase.CustomerId == customer.Id)
                        .Sum(pi => pi.Quantity)
                })
                .Where(c => c.TotalUnits > 0)
                .ToListAsync();

            // Assert
            handlerResult.Should().HaveCount(expectedCategories.Count,
                $"Customer {customer.Id} should have same number of categories");

            var handlerResults = handlerResult.ToList();
            for (var i = 0; i < expectedCategories.Count; i++)
            {
                var expected = expectedCategories[i];
                var actual = handlerResults[i];

                actual.CategoryId.Should().Be(expected.CategoryId,
                    $"Category ID mismatch for customer {customer.Id} at position {i}");
                actual.CategoryName.Should().Be(expected.CategoryName,
                    $"Category name mismatch for customer {customer.Id} at position {i}");
                actual.TotalUnits.Should().Be(expected.TotalUnits,
                    $"Total units mismatch for customer {customer.Id} at position {i}");
            }
        }
    }
}