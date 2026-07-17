using FinSync.Application.Features.Customers.DTOs;

namespace FinSync.Application.Features.Customers.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request);

        Task<IEnumerable<CustomerResponseDto>> GetAllAsync();

        Task<CustomerResponseDto?> GetByIdAsync(int customerId);

        Task<CustomerResponseDto?> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request);

        Task<bool> DeleteCustomerAsync(int customerId);
    }
}