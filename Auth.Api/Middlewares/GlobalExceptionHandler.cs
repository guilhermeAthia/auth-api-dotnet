using Auth.Application.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace Auth.Api.Middlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
    CancellationToken cancellationToken)
  {
    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    httpContext.Response.ContentType = "application/json";

    var response = new ErrorResponse
    {
      HttpStatus = "error",
      HttpStatusCode = httpContext.Response.StatusCode,
      HttpRoute = httpContext.Request.Path.Value,
      HttpMessage = exception.Message,
      Type = exception.GetType().Name,
      TraceId = httpContext.TraceIdentifier,
      StackTrace = exception.StackTrace // Lembre-se de remover em produção
    };
    
    await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
    return true;
  }
}