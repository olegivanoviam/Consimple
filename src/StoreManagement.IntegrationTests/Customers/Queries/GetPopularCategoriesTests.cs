using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.IntegrationTests.Customers.Queries;

public class GetPopularCategoriesTests : TestBase
{
    [Fact]
    public async Task GetPopularCategories_ShouldReturnCorrectData()
    {
        // Arrange
        var customers = await Context.Customers.ToListAsync();
        var customerId = customers.First().Id;
        var query = new GetPopularCategoriesQuery(customerId);
        var handler = new GetPopularCategoriesQueryHandler(Context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();

        // Verify that categories are ordered by total units
        var units = result.Select(c => c.TotalUnits).ToList();
        units.Should().BeInDescendingOrder();

        // Verify that total units is calculated correctly
        foreach (var category in result)
        {
            var actualUnits = await Context.PurchaseItems
                .Where(pi => pi.Product.Category.Id == category.CategoryId && pi.Purchase.CustomerId == customerId)
                .SumAsync(pi => pi.Quantity);

            category.TotalUnits.Should().Be(actualUnits);
        }
    }
} 