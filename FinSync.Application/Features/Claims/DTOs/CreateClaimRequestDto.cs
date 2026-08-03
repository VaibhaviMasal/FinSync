using FinSync.Domain.Enums;

namespace FinSync.Application.Features.Claims.DTOs
{
    public class CreateClaimRequestDto
    {
        public int PolicyId { get; set; }

        public ClaimType ClaimType { get; set; }

        public decimal ClaimAmount { get; set; }

        public DateOnly IncidentDate { get; set; }

        public DateOnly ClaimDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? Remarks { get; set; }
    }
}