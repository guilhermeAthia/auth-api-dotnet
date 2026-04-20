using MediatR;

namespace Auth.Application.Auth.Logout;

public record LogoutCommand(string UserId) : IRequest<Unit>;