using AutoMapper;
using FinSync.Application.DTOs.Customer;
using FinSync.Application.Interfaces;
using FinSync.Domain.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace FinSync.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request)
        {
            // Convert Request DTO to Customer Entity
            var customer = _mapper.Map<FinSync.Domain.Entities.Customer>(request);

            // Save customer to database
            var createdCustomer = await _customerRepository.AddAsync(customer);

            // Convert Customer Entity to Response DTO
            var response = _mapper.Map<CustomerResponseDto>(createdCustomer);

            return response;
        }
    }
}