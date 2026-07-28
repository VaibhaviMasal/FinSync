using FinSync.Application.Features.PremiumPayments.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.PremiumPayments.Interfaces
{
    public interface IPremiumPaymentRepository
    {
        //CRUD operations for PremiumPayment entity
        Task<PremiumPayment> AddAsync(PremiumPayment premiumPayment);

        Task<IEnumerable<PremiumPayment>> GetAllAsync(
            PremiumPaymentQueryParametersDto queryParameters);

        Task<PremiumPayment?> GetByIdAsync(int premiumPaymentId);

        Task<PremiumPayment?> UpdateAsync(
            int premiumPaymentId,
            PremiumPayment premiumPayment);

        Task<bool> DeleteAsync(int premiumPaymentId);

        Task<IEnumerable<PremiumPayment>> SearchAsync(string keyword);

        // Business operations for PremiumPayment entity
        Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId);

        Task<IEnumerable<PremiumPayment>> GetPendingPaymentsAsync();

        Task<IEnumerable<PremiumPayment>> GetOverduePaymentsAsync();

        Task<bool> ExistsReceiptNumberAsync(string receiptNumber);

        Task SaveChangesAsync();
    }
}