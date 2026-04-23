using System.Security.Claims;
using Auth.Api.Constants;
using Auth.Application.Auth.Logout;
using Auth.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route(ApiEndpoints.Auth.Route)]
[Tags(ApiEndpoints.Auth.Tag)]
public class LogoutCommandController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Autentica um usuário e gera os tokens de acesso.
    /// </summary>
    [Authorize]
    [HttpPost(ApiEndpoints.Auth.Logout)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Logout()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
        await mediator.Send(new LogoutCommand(userId));
        return NoContent();
    }
}