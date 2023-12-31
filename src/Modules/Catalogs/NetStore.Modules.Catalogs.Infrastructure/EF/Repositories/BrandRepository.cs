﻿using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Catalogs.Domain.Brand;
using NetStore.Modules.Catalogs.Domain.Repositories;

namespace NetStore.Modules.Catalogs.Infrastructure.EF.Repositories;

internal sealed class BrandRepository : IBrandRepository
{
    private readonly CatalogsDbContext _dbContext;

    public BrandRepository(CatalogsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Brand> GetAsync(long id)
        => _dbContext.Brands.SingleOrDefaultAsync(x => x.Id == id);

    public Task<Brand> GetByNameAsync(string name)
        => _dbContext.Brands.SingleOrDefaultAsync(x => x.Name == name);

    public async Task<IEnumerable<Brand>> GetAll()
        => await _dbContext.Brands.ToListAsync();

    public async Task AddAsync(Brand brand)
    {
        await _dbContext.Brands.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync();
    }
}