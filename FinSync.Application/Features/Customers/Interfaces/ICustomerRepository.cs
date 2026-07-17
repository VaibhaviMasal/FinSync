using FinSync.Application.Features.Customers.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Customers.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> AddAsync(Customer customer);

    

    Task<Customer?> GetByIdAsync(int customerId);

    Task<Customer?> UpdateAsync(int customerId, Customer customer);

    Task<bool> DeleteAsync(int customerId);

    Task<IEnumerable<Customer>> SearchAsync(string keyword);

    Task<IEnumerable<Customer>> GetAllAsync(CustomerQueryParametersDto queryParameters);

}