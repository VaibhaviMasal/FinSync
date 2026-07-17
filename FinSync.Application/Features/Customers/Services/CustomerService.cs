using AutoMapper;
using FinSync.Application.Features.Customers.DTOs;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Customers.Services
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

        // Create Customer
        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request)
        {
            var customer = _mapper.Map<Customer>(request);

            var createdCustomer = await _customerRepository.AddAsync(customer);

            return _mapper.Map<CustomerResponseDto>(createdCustomer);
        }

        // Get All Customers
        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }

        // Get Customer By Id
        public async Task<CustomerResponseDto?> GetByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                return null;

            return _mapper.Map<CustomerResponseDto>(customer);
        }

        // Update Customer

          public async Task<CustomerResponseDto?> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request)
        {
            var customer = _mapper.Map<Customer>(request);

            var updatedCustomer = await _customerRepository.UpdateAsync(customerId, customer);

            if (updatedCustomer == null)
                return null;

            return _mapper.Map<CustomerResponseDto>(updatedCustomer);
        }
    }
    }
