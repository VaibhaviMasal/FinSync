using FinSync.Domain.Enums;

namespace FinSync.Application.Features.Claims.DTOs
{
    public class ClaimQueryParametersDto
    {
        public ClaimStatus? ClaimStatus { get; set; }

        public ClaimType? ClaimType { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "ClaimDate";

        public string SortOrder { get; set; } = "desc";
    }
}