using Auth.Application.Auth.Common;
using MediatR;

namespace Auth.Application.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;