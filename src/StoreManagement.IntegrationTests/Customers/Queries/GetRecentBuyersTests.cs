using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Application.Customers.Queries.GetRecentBuyers;

namespace StoreManagement.IntegrationTests.Customers.Queries;

[TestFixture]
public class GetRecentBuyersTests : TestBase
{
    [Test]
    public async Task GetRecentBuyers_WithRangeOfDays_ShouldReturnCorrectData([Range(0, 100, 1)] int lastNDays)
    {
        // Arrange
        var cutoffDate = DateTime.UtcNow.Date.AddDays(-lastNDays);
        var query = new GetRecentBuyersQuery(lastNDays);
        var handler = new GetRecentBuyersQueryHandler(Context);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();

        // Get expected results using direct LINQ query
        var expectedBuyers = await Context.Customers
            .Where(c => c.Purchases.Any(p => p.Date >= cutoffDate))
            .Select(c => new
            {
                c.Id,
                c.FullName,
                LastPurchaseDate = c.Purchases
                    .OrderByDescending(p => p.Date)
                    .Select(p => p.Date)
                    .First()
            })
            .ToListAsync();

        // Compare results
        result.Should().HaveCount(expectedBuyers.Count, 
            $"Number of buyers in last {lastNDays} days should match the database query");

        foreach (var expected in expectedBuyers)
        {
            var actual = result.Should().ContainSingle(
                b => b.Id == expected.Id,
                $"Customer {expected.FullName} should be in the results for last {lastNDays} days").Subject;

            actual.FullName.Should().Be(expected.FullName);
            actual.LastPurchaseDate.Should().Be(expected.LastPurchaseDate);
        }
    }
} 