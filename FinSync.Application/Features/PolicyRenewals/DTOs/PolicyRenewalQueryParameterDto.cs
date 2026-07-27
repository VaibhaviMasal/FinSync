namespace FinSync.Application.Features.PolicyRenewals.DTOs
{
    public class PolicyRenewalQueryParametersDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; } = "RenewalDate";

        public bool Descending { get; set; }

        public string? Status { get; set; }
    }
}