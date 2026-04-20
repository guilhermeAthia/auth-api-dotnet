using Auth.Domain.Entities;
using Auth.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Persistence;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task Add(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
    
    public async Task Update(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public Task<User?> GetById(Guid userId)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public Task<User?> GetByRefreshToken(string token)
    {
        return context.Users.FirstOrDefaultAsync(u => u.RefreshToken == token);
    }

    public Task<User?> GetByEmail(string email)
    {
        return context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}