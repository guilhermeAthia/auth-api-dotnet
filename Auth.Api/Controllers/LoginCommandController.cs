using Auth.Api.Constants;
using Auth.Application.Auth.Common;
using Auth.Application.Auth.Login;
using Auth.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route(ApiEndpoints.Auth.Route)]
[Tags(ApiEndpoints.Auth.Tag)]
public class LoginCommandController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Autentica um usuário e gera os tokens de acesso.
    /// </summary>
    [AllowAnonymous]
    [HttpPost(ApiEndpoints.Auth.Login)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponse))] 
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var token = await mediator.Send(command);
        return Ok(token);
    }
}