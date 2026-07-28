using FinSync.Application.Features.PolicyRenewals.DTOs;
using FinSync.Application.Features.PolicyRenewals.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PolicyRenewalController : ControllerBase
    {
        private readonly IPolicyRenewalService _service;

        public PolicyRenewalController(IPolicyRenewalService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreatePolicyRenewalRequestDto request)
        {
            var result = await _service.CreatePolicyRenewalAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { renewalId = result.RenewalId },
                result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] PolicyRenewalQueryParametersDto queryParameters)
        {
            var result = await _service.GetAllAsync(queryParameters);

            return Ok(result);
        }

        [HttpGet("{renewalId:int}")]
        public async Task<IActionResult> GetById(int renewalId)
        {
            var result = await _service.GetByIdAsync(renewalId);

            return Ok(result);
        }

        [HttpPut("{renewalId:int}")]
        public async Task<IActionResult> Update(
            int renewalId,
            UpdatePolicyRenewalRequestDto request)
        {
            var result = await _service.UpdatePolicyRenewalAsync(
                renewalId,
                request);

            return Ok(result);
        }

        [HttpDelete("{renewalId:int}")]
        public async Task<IActionResult> Delete(int renewalId)
        {
            await _service.DeletePolicyRenewalAsync(renewalId);

            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string keyword)
        {
            var result = await _service.SearchAsync(keyword);

            return Ok(result);
        }

        [HttpGet("policy/{policyId:int}")]
        public async Task<IActionResult> GetByPolicy(int policyId)
        {
            var result = await _service.GetByPolicyIdAsync(policyId);

            return Ok(result);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingRenewals(
            [FromQuery] int days = 30)
        {
            var result = await _service.GetUpcomingRenewalsAsync(days);

            return Ok(result);
        }

        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredRenewals()
        {
            var result = await _service.GetExpiredRenewalsAsync();

            return Ok(result);
        }
    }
}