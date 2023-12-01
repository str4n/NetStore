namespace NetStore.Modules.Catalogs.Domain.Repositories;

public interface ICategoryRepository
{
    Task<Category.Category> GetAsync(long id);
    Task<Category.Category> GetByCodeAsync(string code);
    Task<IEnumerable<Category.Category>> GetAllAsync();
    Task AddAsync(Category.Category category);
    Task UpdateAsync(Category.Category category);
    Task DeleteAsync(long id);
}