﻿namespace NetStore.Modules.Orders.Domain.Repositories;

public interface IProductRepository
{
    Task<Product.Product> GetAsync(Guid id);
    Task AddAsync(Product.Product product);
    Task UpdateAsync(Product.Product product);
    Task DecreaseStockAsync(Guid productId, int quantity);
    Task SaveChangesAsync(); // this is at least controversial 
}