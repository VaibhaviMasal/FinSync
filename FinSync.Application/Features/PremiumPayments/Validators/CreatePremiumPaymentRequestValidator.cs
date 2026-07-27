using FinSync.Application.Features.PremiumPayments.DTOs;
using FluentValidation;

namespace FinSync.Application.Features.PremiumPayments.Validators
{
    public class CreatePremiumPaymentRequestValidator : AbstractValidator<CreatePremiumPaymentRequestDto>
    {
        public CreatePremiumPaymentRequestValidator()
        {
            RuleFor(x => x.PolicyId)
                .GreaterThan(0)
                .WithMessage("Valid Policy is required.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .WithMessage("Due Date is required.");

            RuleFor(x => x.PaymentMode)
                .IsInEnum()
                .WithMessage("Valid Payment Mode is required.");

            RuleFor(x => x.ReceiptNumber)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.ReceiptNumber));

            RuleFor(x => x.TransactionReference)
                .MaximumLength(100)
                .When(x => !string.IsNullOrWhiteSpace(x.TransactionReference));

            RuleFor(x => x.Remarks)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.Remarks));

            RuleFor(x => x)
                .Must(x => !x.PaymentDate.HasValue || x.PaymentDate.Value >= x.DueDate)
                .WithMessage("Payment Date cannot be earlier than Due Date.");
        }
    }
}