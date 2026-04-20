using Auth.Application.Common.Exceptions;
using Auth.Application.Common.Interfaces;
using Auth.Domain.Interfaces;
using MediatR;

namespace Auth.Application.Auth.Login;

public class LoginHandler(
    IUserRepository repository,
    IJwtService jwtService,
    IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await repository.GetByEmail(command.Email);

        if (user is null || !passwordHasher.Verify(command.Password, user.PasswordHash))
            throw new InvalidUserOrPasswordException();
        
        var accessToken = jwtService.GenerateAccessToken(user.Id.ToString(), user.Email);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        
        await repository.Update(user);

        return new AuthResponse(accessToken, refreshToken);
    }
}