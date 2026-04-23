using Auth.Api.Constants;
using Auth.Application.Auth.Common;
using Auth.Application.Auth.RefreshToken;
using Auth.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route(ApiEndpoints.Auth.Route)]
[Tags(ApiEndpoints.Auth.Tag)]
public class RefreshCommandController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Autentica um usuário e gera os tokens de acesso.
    /// </summary>
    [Authorize]
    [HttpPost(ApiEndpoints.Auth.Refresh)]
    [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(AuthResponse))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Refresh(RefreshTokenCommand command)
    {
        return Ok(await mediator.Send(command));
    }
}