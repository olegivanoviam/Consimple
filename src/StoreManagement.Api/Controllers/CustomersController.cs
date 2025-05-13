using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Application.Customers.Queries.GetBirthdayCustomers;
using StoreManagement.Application.Customers.Queries.GetPopularCategories;
using StoreManagement.Application.Customers.Queries.GetRecentBuyers;

namespace StoreManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("birthday")]
    public async Task<ActionResult<IEnumerable<BirthdayCustomerDto>>> GetBirthdayCustomers([FromQuery] DateTime date)
    {
        var result = await _mediator.Send(new GetBirthdayCustomersQuery(date));
        return Ok(result);
    }

    [HttpGet("recent-buyers")]
    public async Task<ActionResult<IEnumerable<RecentBuyerDto>>> GetRecentBuyers([FromQuery] int days)
    {
        var result = await _mediator.Send(new GetRecentBuyersQuery(days));
        return Ok(result);
    }

    [HttpGet("{customerId}/popular-categories")]
    public async Task<ActionResult<IEnumerable<CategoryPurchaseDto>>> GetPopularCategories(int customerId)
    {
        var result = await _mediator.Send(new GetPopularCategoriesQuery(customerId));
        return Ok(result);
    }
}