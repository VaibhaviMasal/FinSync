using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinSync.Shared.Common;
using FinSync.Application.Features.Policies.DTOs;
using FinSync.Application.Features.Policies.Interfaces;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        // POST: api/Policy
        // Create Policy
        [HttpPost]
        public async Task<IActionResult> CreatePolicy(CreatePolicyRequestDto request)
        {
            var createdPolicy = await _policyService.CreatePolicyAsync(request);

            return CreatedAtAction(
                nameof(GetPolicyById),
                new { id = createdPolicy.PolicyId },
                ApiResponseFactory.Created(
                    createdPolicy,
                    "Policy created successfully."
                ));
        }

        // GET: api/Policy
        // Get All Policies
        [HttpGet]
        public async Task<IActionResult> GetAllPolicies(
            [FromQuery] PolicyQueryParametersDto queryParameters)
        {
            var policies = await _policyService.GetAllAsync(queryParameters);

            return Ok(ApiResponseFactory.Success(
                policies,
                "Policies retrieved successfully."));
        }

        // GET: api/Policy/5
        // Get Policy by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPolicyById(int id)
        {
            var policy = await _policyService.GetByIdAsync(id);

            if (policy == null)
            {
                return NotFound(new
                {
                    Message = $"Policy with Id {id} not found."
                });
            }

            return Ok(ApiResponseFactory.Success(
                policy,
                "Policy retrieved successfully."));
        }

        // PUT: api/Policy/5
        // Update Policy
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePolicy(
            int id,
            [FromBody] UpdatePolicyRequestDto request)
        {
            var policy = await _policyService.UpdatePolicyAsync(id, request);

            if (policy == null)
            {
                return NotFound(
                    ApiResponseFactory.NotFound<object>(
                        $"Policy with Id {id} not found."));
            }

            return Ok(ApiResponseFactory.Success(
                policy,
                "Policy updated successfully."));
        }

        // DELETE: api/Policy/5
        // Delete Policy
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePolicy(int id)
        {
            var deleted = await _policyService.DeletePolicyAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    Message = $"Policy with Id {id} not found."
                });
            }

            return Ok(ApiResponseFactory.Success(
                new { },
                "Policy deleted successfully."));
        }

        // GET: api/Policy/search?keyword=LIC
        // Search Policy
        [HttpGet("search")]
        public async Task<IActionResult> SearchPolicy([FromQuery] string keyword)
        {
            var policies = await _policyService.SearchAsync(keyword);

            return Ok(ApiResponseFactory.Success(
                policies,
                "Search completed successfully."));
        }
    }
}