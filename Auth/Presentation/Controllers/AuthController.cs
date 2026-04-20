using System.Security.Claims;
using Auth.Application.Auth.Login;
using Auth.Application.Auth.Logout;
using Auth.Application.Auth.RefreshToken;
using Auth.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Presentation.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody]LoginCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(token);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(token);
    }
    
    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
    {
        return Ok(await mediator.Send(command));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

        await mediator.Send(new LogoutCommand(userId));

        return NoContent();
    }
}