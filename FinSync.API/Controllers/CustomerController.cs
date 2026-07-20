using FinSync.Shared.Common;
using FinSync.Application.Features.Customers.DTOs;
using FinSync.Application.Features.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST: api/Customer
        // Create Customer
        [HttpPost]
public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDto request)
{
    var createdCustomer = await _customerService.CreateCustomerAsync(request);

    return CreatedAtAction(
        nameof(GetCustomerById),
        new { id = createdCustomer.CustomerId },
        ApiResponseFactory.Success(
            createdCustomer,
            "Customer created successfully."
        ));
}

        // GET: api/Customer
        // GET All Customers
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers(
            [FromQuery] CustomerQueryParametersDto queryParameters)
        {
            var customers = await _customerService.GetAllAsync(queryParameters);

            return Ok(ApiResponseFactory.Success(
    customers,
    "Customers retrieved successfully."));
        }

        // GET: api/Customer/5
        // GET Customer by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    Message = $"Customer with Id {id} not found."
                });
            }

            return Ok(ApiResponseFactory.Success(
     customer,
     "Customer retrieved successfully."));
        }

        // PUT: api/Customer/5
        // Update Customer
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(
            int id,
            [FromBody] UpdateCustomerRequestDto request)
        {
            var customer = await _customerService.UpdateCustomerAsync(id, request);

            if (customer == null)
            {
                return NotFound(new
                {
                    Message = $"Customer with Id {id} not found."
                });
            }

            return Ok(ApiResponseFactory.Success(
    customer,
    "Customer updated successfully."));
        }

        // DELETE: api/Customer/5
        // Delete Customer
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = $"Customer with Id {id} not found."
                });
            }

            return Ok(ApiResponseFactory.Success<object>(
    null,
    "Customer deleted successfully."));
        }

        // GET: api/Customer/search?keyword=aarav

        // Search Customer
        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomer([FromQuery] string keyword)
        {
            var customers = await _customerService.SearchCustomerAsync(keyword);

            return Ok(ApiResponseFactory.Success(
    customers,
    "Search completed successfully."));
        }
    }
}