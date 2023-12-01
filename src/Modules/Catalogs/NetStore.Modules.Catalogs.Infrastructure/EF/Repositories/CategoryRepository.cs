using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Category;
using NetStore.Modules.Catalogs.Domain.Repositories;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    private readonly CatalogsDbContext _dbContext;

    public CategoryRepository(CatalogsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Category> GetAsync(long id)
        => _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == id);
    
    public Task<Category> GetByCodeAsync(string code)
        => _dbContext.Categories.SingleOrDefaultAsync(x => x.Code == code);

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _dbContext.Categories.ToListAsync();

    public async Task AddAsync(Category category)
        => await _dbContext.Categories.AddAsync(category);

    public Task UpdateAsync(Category category)
    {
        _dbContext.Categories.Update(category);
        return Task.CompletedTask;
    }
}