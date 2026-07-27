using FinSync.Application.Features.PremiumPayments.DTOs;

namespace FinSync.Application.Features.PremiumPayments.Interfaces
{
    public interface IPremiumPaymentService
    {
        Task<PremiumPaymentResponseDto> CreatePremiumPaymentAsync(
            CreatePremiumPaymentRequestDto request);

        Task<IEnumerable<PremiumPaymentResponseDto>> GetAllAsync(
            PremiumPaymentQueryParametersDto queryParameters);

        Task<PremiumPaymentResponseDto> GetByIdAsync(int premiumPaymentId);

        Task<PremiumPaymentResponseDto> UpdatePremiumPaymentAsync(
            int premiumPaymentId,
            UpdatePremiumPaymentRequestDto request);

        Task<bool> DeletePremiumPaymentAsync(int premiumPaymentId);

        Task<IEnumerable<PremiumPaymentResponseDto>> SearchAsync(string keyword);

        // Business Methods
        Task<IEnumerable<PremiumPaymentResponseDto>> GetPaymentsByPolicyAsync(int policyId);

        Task<IEnumerable<PremiumPaymentResponseDto>> GetPendingPaymentsAsync();

        Task<IEnumerable<PremiumPaymentResponseDto>> GetOverduePaymentsAsync();
    }
}