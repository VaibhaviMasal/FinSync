using FinSync.Domain.Enums;

namespace FinSync.Domain.Entities
{
    public class PremiumPayment
    {
        public int PremiumPaymentId { get; set; }

        // Foreign Key
        public int PolicyId { get; set; }

        // Payment Details
        public decimal Amount { get; set; }

        public DateOnly DueDate { get; set; }

        public DateOnly? PaymentDate { get; set; }

        public PaymentMode PaymentMode { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public string? ReceiptNumber { get; set; }

        public string? TransactionReference { get; set; }

        public string? Remarks { get; set; }

        // Audit Fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Property
        public Policy Policy { get; set; } = null!;
    }
}