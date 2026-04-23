using Auth.Domain.Interfaces;
using MediatR;

namespace Auth.Application.Auth.Logout;

public class LogoutHandler(IUserRepository repository)
    : IRequestHandler<LogoutCommand, Unit>
{
    public async Task<Unit> Handle(LogoutCommand command, CancellationToken ct)
    {
        var user = await repository.GetById(new Guid(command.UserId));

        if (user is null)
            return Unit.Value;

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        await repository.Update(user);

        return Unit.Value;
    }
}