namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IBrandRepository
{
    Task<Brand.Brand> GetAsync(long id);
    Task AddAsync(Brand.Brand brand);
    Task UpdateAsync(Brand.Brand brand);
}