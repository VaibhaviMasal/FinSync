using FinSync.Application.Features.Claims.DTOs;
using FinSync.Application.Features.Claims.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClaimRequestDto request)
        {
            var claim = await _claimService.CreateClaimAsync(request);

            return CreatedAtAction(nameof(GetById),
                new { claimId = claim.ClaimId }, claim);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] ClaimQueryParametersDto queryParameters)
        {
            var claims = await _claimService.GetAllAsync(queryParameters);

            return Ok(claims);
        }

        [HttpGet("{claimId:int}")]
        public async Task<IActionResult> GetById(int claimId)
        {
            var claim = await _claimService.GetByIdAsync(claimId);

            return Ok(claim);
        }

        [HttpPut("{claimId:int}")]
        public async Task<IActionResult> Update(
            int claimId,
            UpdateClaimRequestDto request)
        {
            var claim = await _claimService.UpdateClaimAsync(claimId, request);

            return Ok(claim);
        }

        [HttpDelete("{claimId:int}")]
        public async Task<IActionResult> Delete(int claimId)
        {
            await _claimService.DeleteClaimAsync(claimId);

            return NoContent();
        }
    }
}