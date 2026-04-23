using Auth.Application.Auth.Common;
using Auth.Application.Common.Interfaces;
using Auth.Domain.Interfaces;
using MediatR;

namespace Auth.Application.Auth.RefreshToken;

public class RefreshTokenHandler(
    IUserRepository repository,
    IJwtService jwtService
) : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var user = await repository.GetByRefreshToken(command.RefreshToken);

        if (user is null ||
            user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            throw new Exception("Invalid refresh token");
        }

        // 🔁 ROTAÇÃO (muito importante)
        var newRefreshToken = jwtService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        await repository.Update(user);

        var accessToken = jwtService.GenerateAccessToken(user.Id.ToString(), user.Email);

        return new AuthResponse(accessToken, newRefreshToken);
    }
}