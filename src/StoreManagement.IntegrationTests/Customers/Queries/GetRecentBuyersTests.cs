using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Application.Customers.Queries.GetRecentBuyers;

namespace StoreManagement.IntegrationTests.Customers.Queries;

[TestFixture]
public class GetRecentBuyersTests : TestBase
{
    [Test]
    public async Task GetRecentBuyers_ShouldReturnCorrectData()
    {
        // Arrange
        var query = new GetRecentBuyersQuery(30); // Last 30 days
        var handler = new GetRecentBuyersQueryHandler(Context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        
        // Verify that buyers are ordered by most recent purchase
        var dates = result.Select(b => b.LastPurchaseDate).ToList();
        Assert.That(dates, Is.Ordered.Descending);

        // Verify that each buyer exists in the database
        foreach (var buyer in result)
        {
            var customer = await Context.Customers.FindAsync(buyer.Id);
            Assert.That(customer, Is.Not.Null);
            Assert.That(customer!.FullName, Is.EqualTo(buyer.FullName));
        }
    }
} 