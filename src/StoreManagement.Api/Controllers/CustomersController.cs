using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;
using StoreManagement.Application.Customers.Queries.GetRecentBuyers;

namespace StoreManagement.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets customers with birthdays on the specified date
    /// </summary>
    [HttpGet("birthday")]
    [ProducesResponseType(typeof(IEnumerable<BirthdayCustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BirthdayCustomerDto>>> GetBirthdayCustomers([FromQuery] DateTime date)
    {
        var result = await _mediator.Send(new GetBirthdayCustomersQuery(date));
        return Ok(result);
    }

    /// <summary>
    /// Gets customers who made purchases in the last N days
    /// </summary>
    [HttpGet("recent-buyers")]
    [ProducesResponseType(typeof(IEnumerable<RecentBuyerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RecentBuyerDto>>> GetRecentBuyers([FromQuery] int days)
    {
        var result = await _mediator.Send(new GetRecentBuyersQuery(days));
        return Ok(result);
    }

    /// <summary>
    /// Gets popular product categories for a specific customer
    /// </summary>
    [HttpGet("{customerId}/popular-categories")]
    [ProducesResponseType(typeof(IEnumerable<CategoryPurchaseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoryPurchaseDto>>> GetPopularCategories(int customerId)
    {
        var result = await _mediator.Send(new GetPopularCategoriesQuery(customerId));
        return Ok(result);
    }
}