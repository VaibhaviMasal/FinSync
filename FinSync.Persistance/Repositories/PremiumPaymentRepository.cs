using FinSync.Application.Features.PremiumPayments.DTOs;
using FinSync.Application.Features.PremiumPayments.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class PremiumPaymentRepository : IPremiumPaymentRepository
    {
        private readonly FinSyncDbContext _context;

        public PremiumPaymentRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<PremiumPayment> AddAsync(PremiumPayment premiumPayment)
        {
            await _context.PremiumPayments.AddAsync(premiumPayment);
            await _context.SaveChangesAsync();

            return premiumPayment;
        }

        public async Task<IEnumerable<PremiumPayment>> GetAllAsync(
            PremiumPaymentQueryParametersDto queryParameters)
        {
            var query = _context.PremiumPayments
                .Include(x => x.Policy)
                .AsQueryable();

            // Filter by Policy
            if (queryParameters.PolicyId.HasValue)
            {
                query = query.Where(x =>
                    x.PolicyId == queryParameters.PolicyId.Value);
            }

            // Filter by Payment Status
            if (!string.IsNullOrWhiteSpace(queryParameters.PaymentStatus))
            {
                query = query.Where(x =>
                    x.PaymentStatus.ToString() ==
                    queryParameters.PaymentStatus);
            }

            // Search by Policy Number
            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                query = query.Where(x =>
                    x.Policy.PolicyNumber.Contains(queryParameters.SearchTerm));
            }

            // Sorting
            query = queryParameters.SortBy?.ToLower() switch
            {
                "amount" => queryParameters.IsDescending
                    ? query.OrderByDescending(x => x.Amount)
                    : query.OrderBy(x => x.Amount),

                "paymentdate" => queryParameters.IsDescending
                    ? query.OrderByDescending(x => x.PaymentDate)
                    : query.OrderBy(x => x.PaymentDate),

                _ => queryParameters.IsDescending
                    ? query.OrderByDescending(x => x.DueDate)
                    : query.OrderBy(x => x.DueDate)
            };

            // Pagination
            query = query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);

            return await query.ToListAsync();
        }

        public async Task<PremiumPayment?> GetByIdAsync(int premiumPaymentId)
        {
            return await _context.PremiumPayments
                .Include(x => x.Policy)
                .FirstOrDefaultAsync(x =>
                    x.PremiumPaymentId == premiumPaymentId);
        }

        public async Task<PremiumPayment?> UpdateAsync(
            int premiumPaymentId,
            PremiumPayment premiumPayment)
        {
            var existingPayment = await _context.PremiumPayments
                .FirstOrDefaultAsync(x =>
                    x.PremiumPaymentId == premiumPaymentId);

            if (existingPayment == null)
                return null;

            _context.Entry(existingPayment)
                .CurrentValues
                .SetValues(premiumPayment);

            existingPayment.PremiumPaymentId = premiumPaymentId;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(premiumPaymentId);
        }

        public async Task<bool> DeleteAsync(int premiumPaymentId)
        {
            var payment = await _context.PremiumPayments
                .FirstOrDefaultAsync(x =>
                    x.PremiumPaymentId == premiumPaymentId);

            if (payment == null)
                return false;

            _context.PremiumPayments.Remove(payment);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PremiumPayment>> SearchAsync(string keyword)
        {
            return await _context.PremiumPayments
                .Include(x => x.Policy)
                .Where(x =>
                    x.Policy.PolicyNumber.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<PremiumPayment>> GetPaymentsByPolicyAsync(int policyId)
        {
            return await _context.PremiumPayments
                .Include(x => x.Policy)
                .Where(x => x.PolicyId == policyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PremiumPayment>> GetPendingPaymentsAsync()
        {
            return await _context.PremiumPayments
                .Include(x => x.Policy)
                .Where(x => x.PaymentStatus == Domain.Enums.PaymentStatus.Pending)
                .ToListAsync();
        }

        public async Task<IEnumerable<PremiumPayment>> GetOverduePaymentsAsync()
        {
            return await _context.PremiumPayments
                .Include(x => x.Policy)
                .Where(x =>
                    x.PaymentStatus == Domain.Enums.PaymentStatus.Overdue)
                .ToListAsync();
        }

        public async Task<bool> ExistsReceiptNumberAsync(string receiptNumber)
        {
            return await _context.PremiumPayments
                .AnyAsync(x => x.ReceiptNumber == receiptNumber);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}