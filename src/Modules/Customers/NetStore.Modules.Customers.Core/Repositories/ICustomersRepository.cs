using NetStore.Modules.Customers.Core.Domain.Customer;

namespace NetStore.Modules.Customers.Core.Repositories;

internal interface ICustomersRepository
{
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task<Customer> GetAsync(Guid id);
}