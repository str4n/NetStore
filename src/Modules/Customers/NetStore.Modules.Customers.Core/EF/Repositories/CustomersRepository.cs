using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Customers.Core.Domain.Customer;
using NetStore.Modules.Customers.Core.Repositories;

namespace NetStore.Modules.Customers.Core.EF.Repositories;

internal sealed class CustomersRepository : ICustomersRepository
{
    private readonly CustomersDbContext _dbContext;
    private readonly DbSet<Customer> _customers;

    public CustomersRepository(CustomersDbContext dbContext)
    {
        _dbContext = dbContext;
        _customers = dbContext.Customers;
    }
    
    public async Task AddAsync(Customer customer)
    {
        await _customers.AddAsync(customer);
    }

    public Task UpdateAsync(Customer customer)
    {
        _customers.Update(customer);
        return Task.CompletedTask;
    }

    public Task<Customer> GetAsync(Guid id)
        => _customers.SingleOrDefaultAsync(x => x.Id == id);
}