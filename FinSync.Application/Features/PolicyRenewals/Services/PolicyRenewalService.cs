using AutoMapper;
using FinSync.Application.Features.Policies.Interfaces;
using FinSync.Application.Features.PolicyRenewals.DTOs;
using FinSync.Application.Features.PolicyRenewals.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Domain.Enums;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.PolicyRenewals.Services
{
    public class PolicyRenewalService : IPolicyRenewalService
    {
        private readonly IPolicyRenewalRepository _repository;
        private readonly IPolicyRepository _policyRepository;
        private readonly IMapper _mapper;

        public PolicyRenewalService(
            IPolicyRenewalRepository repository,
            IPolicyRepository policyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public async Task<PolicyRenewalResponseDto> CreatePolicyRenewalAsync(
            CreatePolicyRenewalRequestDto request)
        {
            var policy = await _policyRepository.GetByIdAsync(request.PolicyId);

            if (policy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {request.PolicyId} was not found.");
            }

            if (request.NewExpiryDate <= DateOnly.FromDateTime(policy.EndDate))
            {
                throw new BadRequestException(
                    "New Expiry Date must be later than the current Policy Expiry Date.");
            }

            if (request.RenewalPremium <= 0)
            {
                throw new BadRequestException(
                    "Renewal Premium must be greater than zero.");
            }

            var renewal = _mapper.Map<PolicyRenewal>(request);

            renewal.OldExpiryDate = DateOnly.FromDateTime(policy.EndDate);
            renewal.CreatedDate = DateTime.UtcNow;
            renewal.RenewalStatus = RenewalStatus.Renewed;

            var createdRenewal = await _repository.AddAsync(renewal);

            return _mapper.Map<PolicyRenewalResponseDto>(createdRenewal);
        }

        public async Task<IEnumerable<PolicyRenewalResponseDto>> GetAllAsync(
            PolicyRenewalQueryParametersDto queryParameters)
        {
            var renewals = await _repository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<PolicyRenewalResponseDto>>(renewals);
        }

        public async Task<PolicyRenewalResponseDto> GetByIdAsync(int renewalId)
        {
            var renewal = await _repository.GetByIdAsync(renewalId);

            if (renewal == null)
            {
                throw new NotFoundException(
                    $"Policy Renewal with ID {renewalId} was not found.");
            }

            return _mapper.Map<PolicyRenewalResponseDto>(renewal);
        }

        public async Task<PolicyRenewalResponseDto> UpdatePolicyRenewalAsync(
            int renewalId,
            UpdatePolicyRenewalRequestDto request)
        {
            var policy = await _policyRepository.GetByIdAsync(request.PolicyId);

            if (policy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {request.PolicyId} was not found.");
            }

            if (request.NewExpiryDate <= DateOnly.FromDateTime(policy.EndDate))
            {
                throw new BadRequestException(
                    "New Expiry Date must be later than the current Policy Expiry Date.");
            }

            if (request.RenewalPremium <= 0)
            {
                throw new BadRequestException(
                    "Renewal Premium must be greater than zero.");
            }

            var renewal = _mapper.Map<PolicyRenewal>(request);

            renewal.OldExpiryDate = DateOnly.FromDateTime(policy.EndDate);
            renewal.UpdatedDate = DateTime.UtcNow;

            var updatedRenewal = await _repository.UpdateAsync(
                renewalId,
                renewal);

            if (updatedRenewal == null)
            {
                throw new NotFoundException(
                    $"Policy Renewal with ID {renewalId} was not found.");
            }

            return _mapper.Map<PolicyRenewalResponseDto>(updatedRenewal);
        }

        public async Task<bool> DeletePolicyRenewalAsync(int renewalId)
        {
            var deleted = await _repository.DeleteAsync(renewalId);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Policy Renewal with ID {renewalId} was not found.");
            }

            return true;
        }

        public async Task<IEnumerable<PolicyRenewalResponseDto>> SearchAsync(string keyword)
        {
            var renewals = await _repository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<PolicyRenewalResponseDto>>(renewals);
        }

        public async Task<IEnumerable<PolicyRenewalResponseDto>> GetByPolicyIdAsync(int policyId)
        {
            var renewals = await _repository.GetByPolicyIdAsync(policyId);

            return _mapper.Map<IEnumerable<PolicyRenewalResponseDto>>(renewals);
        }

        public async Task<IEnumerable<PolicyRenewalResponseDto>> GetUpcomingRenewalsAsync(int days = 30)
        {
            var renewals = await _repository.GetUpcomingRenewalsAsync(days);

            return _mapper.Map<IEnumerable<PolicyRenewalResponseDto>>(renewals);
        }

        public async Task<IEnumerable<PolicyRenewalResponseDto>> GetExpiredRenewalsAsync()
        {
            var renewals = await _repository.GetExpiredRenewalsAsync();

            return _mapper.Map<IEnumerable<PolicyRenewalResponseDto>>(renewals);
        }
    }
}