﻿namespace NetStore.Modules.Catalogs.Domain.Repositories;

internal interface IProductRepository
{
    Task<Product.Product> GetAsync(Guid id);
    Task<IEnumerable<Product.Product>> GetAllAsync();
    Task AddAsync(Product.Product product);
    Task UpdateAsync(Product.Product product);
}