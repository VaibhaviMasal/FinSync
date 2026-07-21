using FinSync.Application.Features.Customers.DTOs;

namespace FinSync.Application.Features.Customers.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request);

        Task<IEnumerable<CustomerResponseDto>> GetAllAsync(CustomerQueryParametersDto queryParameters);

        Task<CustomerResponseDto> GetByIdAsync(int customerId);

        Task<CustomerResponseDto> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request);

        Task<bool> DeleteCustomerAsync(int customerId);

        Task<IEnumerable<CustomerResponseDto>> SearchCustomerAsync(string keyword);
    }
}