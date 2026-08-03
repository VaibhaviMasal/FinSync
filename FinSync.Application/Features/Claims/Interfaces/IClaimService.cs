using FinSync.Application.Features.Claims.DTOs;

namespace FinSync.Application.Features.Claims.Interfaces
{
    public interface IClaimService
    {
        Task<ClaimResponseDto> CreateClaimAsync(CreateClaimRequestDto request);

        Task<ClaimResponseDto?> GetByIdAsync(int claimId);

        Task<IEnumerable<ClaimResponseDto>> GetAllAsync(ClaimQueryParametersDto queryParameters);

        Task<ClaimResponseDto> UpdateClaimAsync(int claimId, UpdateClaimRequestDto request);

        Task DeleteClaimAsync(int claimId);
    }
}