using Auth.Application.Common.Interfaces;
using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using MediatR;

namespace Auth.Application.Auth.Register;

public class RegisterHandler(IUserRepository repostory, IPasswordHasher hasher, IJwtService jwtService) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand command, CancellationToken ct)
    {
        var existingUser = await repostory.GetByEmail(command.Email);

        if (existingUser is not null)
            throw new Exception("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            PasswordHash = hasher.Hash(command.Password),
            FirstName = command.FirstName,
            LastName = command.LastName,
        };

        await repostory.Add(user);

        return jwtService.GenerateAccessToken(user.Id.ToString(), user.Email);
    }
}