using AutoMapper;
using FinSync.Application.Features.Claims.DTOs;
using FinSync.Application.Features.Claims.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Claims.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IClaimRepository _claimRepository;
        private readonly IMapper _mapper;

        public ClaimService(
            IClaimRepository claimRepository,
            IMapper mapper)
        {
            _claimRepository = claimRepository;
            _mapper = mapper;
        }

        // Create Claim
        public async Task<ClaimResponseDto> CreateClaimAsync(CreateClaimRequestDto request)
        {
            var claim = _mapper.Map<InsuranceClaim>(request);

            claim.ClaimNumber = $"CLM-{DateTime.UtcNow:yyyyMMddHHmmss}";

            var createdClaim = await _claimRepository.AddAsync(claim);

            return _mapper.Map<ClaimResponseDto>(createdClaim);
        }

        // Get All Claims
        public async Task<IEnumerable<ClaimResponseDto>> GetAllAsync(ClaimQueryParametersDto queryParameters)
        {
            var claims = await _claimRepository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<ClaimResponseDto>>(claims);
        }

        // Get Claim By Id
        public async Task<ClaimResponseDto?> GetByIdAsync(int claimId)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new NotFoundException($"Claim with ID {claimId} was not found.");

            return _mapper.Map<ClaimResponseDto>(claim);
        }

        // Update Claim
        public async Task<ClaimResponseDto> UpdateClaimAsync(int claimId, UpdateClaimRequestDto request)
        {
            var claim = _mapper.Map<InsuranceClaim>(request);

            var updatedClaim = await _claimRepository.UpdateAsync(claimId, claim);

            if (updatedClaim == null)
                throw new NotFoundException($"Claim with ID {claimId} was not found.");

            return _mapper.Map<ClaimResponseDto>(updatedClaim);
        }

        // Delete Claim
        public async Task DeleteClaimAsync(int claimId)
        {
            var deleted = await _claimRepository.DeleteAsync(claimId);

            if (!deleted)
                throw new NotFoundException($"Claim with ID {claimId} was not found.");
        }
    }
}