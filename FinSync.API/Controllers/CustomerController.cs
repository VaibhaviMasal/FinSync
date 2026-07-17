using FinSync.Application.DTOs.Customer;
using FinSync.Application.Interfaces;
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

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(
    [FromBody] CreateCustomerRequestDto request)
        {
            var response = await _customerService.CreateCustomerAsync(request);
            return Ok(response);
        }
    }
}