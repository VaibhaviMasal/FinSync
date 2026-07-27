namespace FinSync.Application.Features.PremiumPayments.DTOs
{
    public class PremiumPaymentQueryParametersDto
    {
        public string? SearchTerm { get; set; }

        public string? SortBy { get; set; } = "DueDate";

        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public int? PolicyId { get; set; }

        public string? PaymentStatus { get; set; }
    }
}