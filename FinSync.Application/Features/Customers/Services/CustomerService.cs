using AutoMapper;
using FinSync.Application.Features.AuditLogs.Interfaces; // ✅ ADD
using FinSync.Application.Features.Customers.DTOs;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IAuditLogService _auditLogService; // ✅ ADD

        public CustomerService(
            ICustomerRepository customerRepository,
            IMapper mapper,
            IAuditLogService auditLogService) // ✅ ADD
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _auditLogService = auditLogService; // ✅ ADD
        }

        // Create Customer
        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerRequestDto request)
        {
            var customer = _mapper.Map<Customer>(request);

            var createdCustomer = await _customerRepository.AddAsync(customer);

            // ✅ AUDIT LOG
            await _auditLogService.AddAsync(
                "Customer Created",
                "Customer",
                createdCustomer.CustomerId,
                "Admin"
            );

            return _mapper.Map<CustomerResponseDto>(createdCustomer);
        }

        // Get All Customers
        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync(CustomerQueryParametersDto queryParameters)
        {
            var customers = await _customerRepository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }

        // Get Customer By Id
        public async Task<CustomerResponseDto> GetByIdAsync(int customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);

            if (customer == null)
                throw new NotFoundException($"Customer with ID {customerId} was not found.");

            return _mapper.Map<CustomerResponseDto>(customer);
        }

        // Update Customer
        public async Task<CustomerResponseDto> UpdateCustomerAsync(int customerId, UpdateCustomerRequestDto request)
        {
            var customer = _mapper.Map<Customer>(request);

            var updatedCustomer = await _customerRepository.UpdateAsync(customerId, customer);

            if (updatedCustomer == null)
                throw new NotFoundException($"Customer with ID {customerId} was not found.");

            // ✅ AUDIT LOG
            await _auditLogService.AddAsync(
                "Customer Updated",
                "Customer",
                customerId,
                "Admin"
            );

            return _mapper.Map<CustomerResponseDto>(updatedCustomer);
        }

        // Delete Customer
        public async Task<bool> DeleteCustomerAsync(int customerId)
        {
            var deleted = await _customerRepository.DeleteAsync(customerId);

            if (!deleted)
                throw new NotFoundException($"Customer with ID {customerId} was not found.");

            // ✅ AUDIT LOG
            await _auditLogService.AddAsync(
                "Customer Deleted",
                "Customer",
                customerId,
                "Admin"
            );

            return true;
        }

        // Search Customer
        public async Task<IEnumerable<CustomerResponseDto>> SearchCustomerAsync(string keyword)
        {
            var customers = await _customerRepository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<CustomerResponseDto>>(customers);
        }
    }
}