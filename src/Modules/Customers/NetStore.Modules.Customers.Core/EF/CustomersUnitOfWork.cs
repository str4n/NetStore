using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Customers.Core.EF;

internal sealed class CustomersUnitOfWork : PostgresUnitOfWork<CustomersDbContext>, ICustomersUnitOfWork
{
    public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
    {
    }
}