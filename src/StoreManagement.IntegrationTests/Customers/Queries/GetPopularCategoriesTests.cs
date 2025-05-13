using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;

namespace StoreManagement.IntegrationTests.Customers.Queries;

[TestFixture]
public class GetPopularCategoriesTests : TestBase
{
    [Test]
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
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);

        // Verify that categories are ordered by total units
        var units = result.Select(c => c.TotalUnits).ToList();
        Assert.That(units, Is.Ordered.Descending);

        // Verify that total units is calculated correctly
        foreach (var category in result)
        {
            var actualUnits = await Context.PurchaseItems
                .Where(pi => pi.Product.Category.Id == category.CategoryId && pi.Purchase.CustomerId == customerId)
                .SumAsync(pi => pi.Quantity);

            Assert.That(category.TotalUnits, Is.EqualTo(actualUnits));
        }
    }
} 