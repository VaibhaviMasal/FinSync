using FinSync.Application.Features.PremiumPayments.DTOs;
using FinSync.Application.Features.PremiumPayments.Interfaces;
using FinSync.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PremiumPaymentController : ControllerBase
    {
        private readonly IPremiumPaymentService _premiumPaymentService;

        public PremiumPaymentController(IPremiumPaymentService premiumPaymentService)
        {
            _premiumPaymentService = premiumPaymentService;
        }

        // POST: api/PremiumPayment
        [HttpPost]
        public async Task<IActionResult> CreatePremiumPayment(
            CreatePremiumPaymentRequestDto request)
        {
            var createdPayment =
                await _premiumPaymentService.CreatePremiumPaymentAsync(request);

            return CreatedAtAction(
                nameof(GetPremiumPaymentById),
                new { id = createdPayment.PremiumPaymentId },
                ApiResponseFactory.Created(
                    createdPayment,
                    "Premium payment created successfully."));
        }

        // GET: api/PremiumPayment
        [HttpGet]
        public async Task<IActionResult> GetAllPremiumPayments(
            [FromQuery] PremiumPaymentQueryParametersDto queryParameters)
        {
            var payments =
                await _premiumPaymentService.GetAllAsync(queryParameters);

            return Ok(ApiResponseFactory.Success(
                payments,
                "Premium payments retrieved successfully."));
        }

        // GET: api/PremiumPayment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPremiumPaymentById(int id)
        {
            var payment =
                await _premiumPaymentService.GetByIdAsync(id);

            if (payment == null)
            {
                return NotFound(
                    ApiResponseFactory.NotFound<object>(
                        $"Premium Payment with Id {id} not found."));
            }

            return Ok(ApiResponseFactory.Success(
                payment,
                "Premium payment retrieved successfully."));
        }

        // PUT: api/PremiumPayment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePremiumPayment(
            int id,
            [FromBody] UpdatePremiumPaymentRequestDto request)
        {
            var payment =
                await _premiumPaymentService.UpdatePremiumPaymentAsync(id, request);

            if (payment == null)
            {
                return NotFound(
                    ApiResponseFactory.NotFound<object>(
                        $"Premium Payment with Id {id} not found."));
            }

            return Ok(ApiResponseFactory.Success(
                payment,
                "Premium payment updated successfully."));
        }

        // DELETE: api/PremiumPayment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePremiumPayment(int id)
        {
            var deleted =
                await _premiumPaymentService.DeletePremiumPaymentAsync(id);

            if (!deleted)
            {
                return NotFound(
                    ApiResponseFactory.NotFound<object>(
                        $"Premium Payment with Id {id} not found."));
            }

            return Ok(ApiResponseFactory.Success(
                new { },
                "Premium payment deleted successfully."));
        }

        // GET: api/PremiumPayment/search?keyword=LIC
        [HttpGet("search")]
        public async Task<IActionResult> SearchPremiumPayments(
            [FromQuery] string keyword)
        {
            var payments =
                await _premiumPaymentService.SearchAsync(keyword);

            return Ok(ApiResponseFactory.Success(
                payments,
                "Search completed successfully."));
        }

        // GET: api/PremiumPayment/policy/5
        [HttpGet("policy/{policyId}")]
        public async Task<IActionResult> GetPaymentsByPolicy(int policyId)
        {
            var payments =
                await _premiumPaymentService.GetPaymentsByPolicyAsync(policyId);

            return Ok(ApiResponseFactory.Success(
                payments,
                "Policy premium payments retrieved successfully."));
        }

        // GET: api/PremiumPayment/pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingPayments()
        {
            var payments =
                await _premiumPaymentService.GetPendingPaymentsAsync();

            return Ok(ApiResponseFactory.Success(
                payments,
                "Pending premium payments retrieved successfully."));
        }

        // GET: api/PremiumPayment/overdue
        [HttpGet("overdue")]
        public async Task<IActionResult> GetOverduePayments()
        {
            var payments =
                await _premiumPaymentService.GetOverduePaymentsAsync();

            return Ok(ApiResponseFactory.Success(
                payments,
                "Overdue premium payments retrieved successfully."));
        }
    }
}