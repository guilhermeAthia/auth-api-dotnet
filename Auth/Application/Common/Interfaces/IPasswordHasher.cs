namespace Auth.Application.Common.Interfaces;

public interface IPasswordHasher
{
    public string Hash(string password);
    bool Verify(string password, string hash);
}