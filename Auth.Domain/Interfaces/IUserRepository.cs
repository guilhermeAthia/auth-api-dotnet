using Auth.Domain.Entities;

namespace Auth.Domain.Interfaces;

public interface IUserRepository
{
    Task Add(User user);
    
    Task Update(User user);
    
    Task<User?> GetById(Guid userId);

    Task<User?> GetByRefreshToken(string token);
        
    Task<User?> GetByEmail(string email);
}