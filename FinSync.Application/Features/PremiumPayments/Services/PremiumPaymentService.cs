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

        public async Task<IEnumerable<PremiumPaymentResponseDto>> GetAllAsync(
    PremiumPaymentQueryParametersDto queryParameters)
        {
            var payments = await _repository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<PremiumPaymentResponseDto>>(payments);
        }

        public async Task<PremiumPaymentResponseDto> GetByIdAsync(int premiumPaymentId)
        {
            var payment = await _repository.GetByIdAsync(premiumPaymentId);

            if (payment == null)
            {
                throw new NotFoundException(
                    $"Premium Payment with ID {premiumPaymentId} was not found.");
            }

            return _mapper.Map<PremiumPaymentResponseDto>(payment);
        }

        public async Task<PremiumPaymentResponseDto> UpdatePremiumPaymentAsync(
    int premiumPaymentId,
    UpdatePremiumPaymentRequestDto request)
        {
            var policy = await _policyRepository.GetByIdAsync(request.PolicyId);

            if (policy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {request.PolicyId} was not found.");
            }

            if (request.DueDate < DateOnly.FromDateTime(policy.StartDate))
            {
                throw new BadRequestException(
                    "Due Date cannot be earlier than the Policy Start Date.");
            }

            if (request.Amount <= 0)
            {
                throw new BadRequestException(
                    "Amount must be greater than zero.");
            }

            var payment = _mapper.Map<PremiumPayment>(request);

            // Preserve business rules
            payment.ReceiptNumber = $"RCPT-{DateTime.UtcNow:yyyyMMddHHmmss}";
            payment.UpdatedDate = DateTime.UtcNow;

            if (request.PaymentDate.HasValue)
            {
                payment.PaymentStatus = PaymentStatus.Paid;
            }
            else if (request.DueDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                payment.PaymentStatus = PaymentStatus.Overdue;
            }
            else
            {
                payment.PaymentStatus = PaymentStatus.Pending;
            }

            var updatedPayment = await _repository.UpdateAsync(
                premiumPaymentId,
                payment);

            if (updatedPayment == null)
            {
                throw new NotFoundException(
                    $"Premium Payment with ID {premiumPaymentId} was not found.");
            }

            return _mapper.Map<PremiumPaymentResponseDto>(updatedPayment);
        }

        public async Task<bool> DeletePremiumPaymentAsync(int premiumPaymentId)
        {
            var deleted = await _repository.DeleteAsync(premiumPaymentId);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Premium Payment with ID {premiumPaymentId} was not found.");
            }

            return true;
        }

        public async Task<IEnumerable<PremiumPaymentResponseDto>> SearchAsync(string keyword)
        {
            var payments = await _repository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<PremiumPaymentResponseDto>>(payments);
        }

        public async Task<IEnumerable<PremiumPaymentResponseDto>> GetPaymentsByPolicyAsync(int policyId)
        {
            var payments = await _repository.GetPaymentsByPolicyAsync(policyId);

            return _mapper.Map<IEnumerable<PremiumPaymentResponseDto>>(payments);
        }

        public async Task<IEnumerable<PremiumPaymentResponseDto>> GetPendingPaymentsAsync()
        {
            var payments = await _repository.GetPendingPaymentsAsync();

            return _mapper.Map<IEnumerable<PremiumPaymentResponseDto>>(payments);
        }

        public async Task<IEnumerable<PremiumPaymentResponseDto>> GetOverduePaymentsAsync()
        {
            var payments = await _repository.GetOverduePaymentsAsync();

            return _mapper.Map<IEnumerable<PremiumPaymentResponseDto>>(payments);
        }
    }
}