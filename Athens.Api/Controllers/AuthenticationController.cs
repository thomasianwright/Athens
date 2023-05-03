using Athens.Abstractions.Identity.Commands;
using Athens.Abstractions.Identity.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace Athens.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Login(UserLoginQuery loginQuery)
    {
        try
        {
            var response = await _mediator.Send(loginQuery);
        
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public async Task<IActionResult> Register(CreateUserCommand userCommand)
    {
        try
        {
            await _mediator.Send(userCommand);
        
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}