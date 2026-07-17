using FinSync.Application.DTOs.Customer;


namespace FinSync.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request);
}
