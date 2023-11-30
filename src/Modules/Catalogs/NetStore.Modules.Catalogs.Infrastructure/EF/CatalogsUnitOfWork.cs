using NetStore.Shared.Infrastructure.Postgres;

namespace NetStore.Modules.Catalogs.Infrastructure.EF;

internal sealed class CatalogsUnitOfWork : PostgresUnitOfWork<CatalogsDbContext>, ICatalogsUnitOfWork
{
    public CatalogsUnitOfWork(CatalogsDbContext dbContext) : base(dbContext)
    {
    }
}