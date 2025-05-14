using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;

namespace StoreManagement.IntegrationTests.Customers.Queries;

[TestFixture]
public class GetBirthdayCustomersTests : TestBase
{
    private GetBirthdayCustomersQueryHandler _handler;

    [OneTimeSetUp]
    public void SetUpTestData()
    {
        _handler = new GetBirthdayCustomersQueryHandler(Context);
    }

    [Test]
    public async Task GetBirthdayCustomers_ShouldReturnCorrectDataForAllDatesInYear()
    {
        // We'll use 2024 as it's a leap year to test February 29
        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);
        var currentDate = startDate;

        while (currentDate <= endDate)
        {
            // Arrange
            var query = new GetBirthdayCustomersQuery(currentDate);

            // Get results from handler
            var handlerResult = await _handler.Handle(query, CancellationToken.None);

            // Get expected results directly from database
            var expectedCustomers = await Context.Customers
                .Where(c => c.DateOfBirth.Month == currentDate.Month && 
                           c.DateOfBirth.Day == currentDate.Day)
                .Select(c => new BirthdayCustomerDto(c.Id, c.FullName))
                .ToListAsync();

            // Compare lists element by element
            handlerResult.ToList()
                .Should().BeEquivalentTo(expectedCustomers, 
                    $"Customers with birthday on {currentDate:yyyy-MM-dd}");

            currentDate = currentDate.AddDays(1);
        }
    }
} 