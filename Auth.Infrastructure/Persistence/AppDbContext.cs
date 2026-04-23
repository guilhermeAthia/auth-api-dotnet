using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}