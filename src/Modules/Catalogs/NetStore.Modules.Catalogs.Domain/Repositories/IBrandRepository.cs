namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface IBrandRepository
{
    Task<Brand.Brand> GetAsync(long id);
    Task<Brand.Brand> GetByNameAsync(string name);
    Task<IEnumerable<Brand.Brand>> GetAll();
    Task AddAsync(Brand.Brand brand);
    Task UpdateAsync(Brand.Brand brand);
}