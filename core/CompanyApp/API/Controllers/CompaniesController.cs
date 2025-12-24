using CompanyApp.Application.Companies;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CompanyApp.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        var result = await _mediator.Send(new SearchCompaniesQuery(name));
        return Ok(result);
    }
}