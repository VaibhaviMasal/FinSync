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
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequestDto request)
        {
            var createdCustomer = await _customerService.CreateCustomerAsync(request);

            return CreatedAtAction(
                nameof(GetCustomerById),
                new { id = createdCustomer.CustomerId },
                createdCustomer);
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();

            return Ok(customers);
        }

        // GET: api/Customer/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
                return NotFound(new
                {
                    Message = $"Customer with Id {id} not found."
                });

            return Ok(customer);

        } 

        // PUT: api/Customer/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerRequestDto request)
            {
                var customer = await _customerService.UpdateCustomerAsync(id, request);

                if (customer == null)
                {
                    return NotFound(new
                    {
                        Message = $"Customer with Id {id} not found."
                    });
                }

                return Ok(customer);
            }



        // DELETE : api/Customer/{id}
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

            return NoContent();
        }
    }
    }
