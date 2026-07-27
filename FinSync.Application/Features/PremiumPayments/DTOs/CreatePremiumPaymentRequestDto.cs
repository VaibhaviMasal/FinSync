using FinSync.Domain.Enums;

namespace FinSync.Application.Features.PremiumPayments.DTOs
{
    public class CreatePremiumPaymentRequestDto
    {
        public int PolicyId { get; set; }

        public decimal Amount { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public PaymentMode PaymentMode { get; set; }

        public string? ReceiptNumber { get; set; }

        public string? TransactionReference { get; set; }

        public string? Remarks { get; set; }
    }
}