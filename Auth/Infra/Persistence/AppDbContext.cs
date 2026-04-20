using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infra.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}