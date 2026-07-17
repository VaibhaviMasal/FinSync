using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Customers.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);

    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int customerId);

    Task<Customer> UpdateAsync(Customer customer);

    Task<bool> DeleteAsync(int customerId);
}