using FinSync.Domain.Enums;

namespace FinSync.Application.Features.PremiumPayments.DTOs
{
    public class PremiumPaymentResponseDto
    {
        public int PremiumPaymentId { get; set; }

        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public PaymentMode PaymentMode { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string? ReceiptNumber { get; set; }

        public string? TransactionReference { get; set; }

        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}