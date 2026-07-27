using AutoMapper;
using FinSync.Application.Features.Policies.Interfaces;
using FinSync.Application.Features.PremiumPayments.DTOs;
using FinSync.Application.Features.PremiumPayments.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Domain.Enums;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.PremiumPayments.Services
{
    public class PremiumPaymentService : IPremiumPaymentService
    {
        private readonly IPremiumPaymentRepository _repository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public PremiumPaymentService(
            IPremiumPaymentRepository repository,
            IPolicyRepository policyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public async Task<PremiumPaymentResponseDto> CreatePremiumPaymentAsync(
    CreatePremiumPaymentRequestDto request)
        {
            // Check if Policy exists
            var policy = await _policyRepository.GetByIdAsync(request.PolicyId);

            if (policy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {request.PolicyId} was not found.");
            }

            // Validate Due Date
            if (request.DueDate < DateOnly.FromDateTime(policy.StartDate))
            {
                throw new BadRequestException(
                    "Due Date cannot be earlier than the Policy Start Date.");
            }

            // Validate Amount
            if (request.Amount <= 0)
            {
                throw new BadRequestException(
                    "Amount must be greater than zero.");
            }

            // Map DTO to Entity
            var premiumPayment = _mapper.Map<PremiumPayment>(request);

            // Auto-generate Receipt Number
            premiumPayment.ReceiptNumber =
                $"RCPT-{DateTime.UtcNow:yyyyMMddHHmmss}";

            // Determine Payment Status
            if (request.PaymentDate.HasValue)
            {
                premiumPayment.PaymentStatus = PaymentStatus.Paid;
            }
            else if (request.DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                premiumPayment.PaymentStatus = PaymentStatus.Overdue;
            }
            else
            {
                premiumPayment.PaymentStatus = PaymentStatus.Pending;
            }

            premiumPayment.CreatedDate = DateTime.UtcNow;

            var createdPayment = await _repository.AddAsync(premiumPayment);

            return _mapper.Map<PremiumPaymentResponseDto>(createdPayment);
        }

        public Task<IEnumerable<PremiumPaymentResponseDto>> GetAllAsync(PremiumPaymentQueryParametersDto queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<PremiumPaymentResponseDto> GetByIdAsync(int premiumPaymentId)
        {
            throw new NotImplementedException();
        }

        public Task<PremiumPaymentResponseDto> UpdatePremiumPaymentAsync(int premiumPaymentId, UpdatePremiumPaymentRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePremiumPaymentAsync(int premiumPaymentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PremiumPaymentResponseDto>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PremiumPaymentResponseDto>> GetPaymentsByPolicyAsync(int policyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PremiumPaymentResponseDto>> GetPendingPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PremiumPaymentResponseDto>> GetOverduePaymentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}