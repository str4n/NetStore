using Microsoft.EntityFrameworkCore;

namespace NetStore.Shared.Infrastructure.Postgres;

public abstract class PostgresUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public PostgresUnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}