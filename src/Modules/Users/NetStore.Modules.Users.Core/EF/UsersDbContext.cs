using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Users.Core.Domain.User;

namespace NetStore.Modules.Users.Core.EF;

internal sealed class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ActivationToken> ActivationTokens { get; set; }
    public DbSet<RecoveryToken> RecoveryTokens { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("users");
    }
}