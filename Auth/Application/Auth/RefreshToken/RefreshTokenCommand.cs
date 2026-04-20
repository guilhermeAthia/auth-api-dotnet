using MediatR;

namespace Auth.Application.Auth.RefreshToken;

public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponse>;