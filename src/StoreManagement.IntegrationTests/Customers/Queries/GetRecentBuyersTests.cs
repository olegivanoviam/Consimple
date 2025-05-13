using FluentAssertions;
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
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        
        // Verify that buyers are ordered by most recent purchase
        var dates = result.Select(b => b.LastPurchaseDate).ToList();
        dates.Should().BeInDescendingOrder();

        // Verify that each buyer exists in the database
        foreach (var buyer in result)
        {
            var customer = await Context.Customers.FindAsync(buyer.Id);
            customer.Should().NotBeNull();
            customer!.FullName.Should().Be(buyer.FullName);

            // Verify last purchase date
            var lastPurchase = await Context.Purchases
                .Where(p => p.CustomerId == buyer.Id)
                .OrderByDescending(p => p.Date)
                .FirstOrDefaultAsync();

            lastPurchase.Should().NotBeNull();
            buyer.LastPurchaseDate.Should().Be(lastPurchase!.Date);
        }
    }
} 