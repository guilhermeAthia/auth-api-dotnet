namespace Auth.Application.Common.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(string userId, string email);

    string GenerateRefreshToken();
}