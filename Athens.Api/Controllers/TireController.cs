using Athens.Abstractions.Products.Commands;
using Athens.Abstractions.Products.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Athens.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TireController : ControllerBase
{
    private readonly IMediator _mediator;

    public TireController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<IActionResult> GetTires(GetTireProductsQuery query)
    {
        var tires = await _mediator.Send(query);
        
        return Ok(tires);
    }
    
    public async Task<IActionResult> CreateTires(CreateTiresCommand command)
    {
        await _mediator.Send(command);
        
        return Ok();
    }
}