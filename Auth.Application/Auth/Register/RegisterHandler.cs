using Auth.Application.Auth.Common;
using Auth.Application.Common.Exceptions;
using Auth.Application.Common.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using MediatR;

namespace Auth.Application.Auth.Register;

public class RegisterHandler(
        IUserRepository repository, 
        IPasswordHasher hasher, 
        IJwtService jwtService
    ) : IRequestHandler<RegisterCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RegisterCommand command, CancellationToken ct)
    {
        var existingUser = await repository.GetByEmail(command.Email);

        if (existingUser is not null)
            throw new InvalidUserOrPasswordException();

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            PasswordHash = hasher.Hash(command.Password),
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        var accessToken = jwtService.GenerateAccessToken(user.Id.ToString(), user.Email);
        var refreshToken = jwtService.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        
        await repository.Add(user);
        
        return new AuthResponse(accessToken, refreshToken);
    }
}