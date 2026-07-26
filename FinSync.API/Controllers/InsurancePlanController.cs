using FinSync.Application.Features.InsurancePlans.DTOs;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanService _service;

        public InsurancePlanController(IInsurancePlanService service)
        {
            _service = service;
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateInsurancePlanRequestDto request)
        {
            var result = await _service.CreatePlanAsync(request);

            return Ok(ApiResponseFactory.Success(
                result,
                "Insurance Plan created successfully."));
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] InsurancePlanQueryParametersDto queryParameters)
        {
            var result = await _service.GetAllAsync(queryParameters);

            return Ok(ApiResponseFactory.Success(
    result,
    "Insurance Plans retrieved successfully."));
        }

        // GET BY ID
        [HttpGet("{planId}")]
        public async Task<IActionResult> GetById(int planId)
        {
            var result = await _service.GetByIdAsync(planId);

            return Ok(ApiResponseFactory.Success(
    result,
    "Insurance Plan retrieved successfully."));
        }

        // PUT
        [HttpPut("{planId}")]
        public async Task<IActionResult> Update(
            int planId,
            UpdateInsurancePlanRequestDto request)
        {
            var result = await _service.UpdatePlanAsync(planId, request);

            return Ok(ApiResponseFactory.Success(
                result,
                "Insurance Plan updated successfully."));
        }

        // DELETE
        [HttpDelete("{planId}")]
        public async Task<IActionResult> Delete(int planId)
        {
            await _service.DeletePlanAsync(planId);

            return Ok(ApiResponseFactory.Success(
                true,
                "Insurance Plan deleted successfully."));
        }

        // SEARCH
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string keyword)
        {
            var result = await _service.SearchAsync(keyword);

            return Ok(ApiResponseFactory.Success(
     result,
     "Insurance Plans retrieved successfully."));
        }
    }
}