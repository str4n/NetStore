﻿using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;

namespace NetStore.Modules.Customers.Core.EF.Repositories;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly CustomersDbContext _dbContext;
    private readonly DbSet<Customer> _customers;

    public CustomerRepository(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
        _customers = dbContext.Customers;
    }
    
    public async Task AddAsync(Customer customer)
    {
        await _customers.AddAsync(customer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer customer)
    {
        _customers.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Customer> GetAsync(Guid id)
        => _customers
            .Include(x => x.Orders)
            .ThenInclude(x => x.Lines)
            .SingleOrDefaultAsync(x => x.Id == id);
}