using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Users.Core.EF;

internal sealed class UsersUnitOfWork : PostgresUnitOfWork<UsersDbContext>, IUsersUnitOfWork
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
    }
}