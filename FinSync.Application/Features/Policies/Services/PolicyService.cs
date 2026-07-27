using AutoMapper;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Application.Features.Policies.DTOs;
using FinSync.Application.Features.Policies.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Policies.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IPolicyRepository _repository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IInsuranceCompanyRepository _companyRepository;
        private readonly IInsurancePlanRepository _planRepository;
        private readonly IMapper _mapper;

        public PolicyService(
            IPolicyRepository repository,
            ICustomerRepository customerRepository,
            IInsuranceCompanyRepository companyRepository,
            IInsurancePlanRepository planRepository,
            IMapper mapper)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public async Task<PolicyResponseDto> CreatePolicyAsync(
    CreatePolicyRequestDto request)
        {
            // Customer Exists
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException(
                    $"Customer with ID {request.CustomerId} was not found.");
            }

            // Company Exists
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {request.CompanyId} was not found.");
            }

            // Plan Exists
            var plan = await _planRepository.GetByIdAsync(request.PlanId);

            if (plan == null)
            {
                throw new NotFoundException(
                    $"Insurance Plan with ID {request.PlanId} was not found.");
            }

            // Plan belongs to Company
            if (plan.CompanyId != request.CompanyId)
            {
                throw new BadRequestException(
                    "The selected Insurance Plan does not belong to the selected Insurance Company.");
            }

            // Duplicate Policy Number
            if (await _repository.ExistsByPolicyNumberAsync(request.PolicyNumber))
            {
                throw new ConflictException(
                    $"Policy Number '{request.PolicyNumber}' already exists.");
            }

            // Premium Validation
            if (request.PremiumAmount <= 0)
            {
                throw new BadRequestException(
                    "Premium Amount must be greater than zero.");
            }

            // Sum Assured Validation
            if (request.SumAssured < plan.MinimumSumAssured ||
                request.SumAssured > plan.MaximumSumAssured)
            {
                throw new BadRequestException(
                    $"Sum Assured must be between {plan.MinimumSumAssured} and {plan.MaximumSumAssured}.");
            }

            // Date Validation
            if (request.StartDate > request.EndDate)
            {
                throw new BadRequestException(
                    "Start Date cannot be later than End Date.");
            }

            var policy = _mapper.Map<Policy>(request);

            var createdPolicy = await _repository.AddAsync(policy);

            return _mapper.Map<PolicyResponseDto>(createdPolicy);
        }

        public async Task<IEnumerable<PolicyResponseDto>> GetAllAsync(
     PolicyQueryParametersDto queryParameters)
        {
            var policies = await _repository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }

        public async Task<PolicyResponseDto> GetByIdAsync(int policyId)
        {
            var policy = await _repository.GetByIdAsync(policyId);

            if (policy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {policyId} was not found.");
            }

            return _mapper.Map<PolicyResponseDto>(policy);
        }

        public async Task<PolicyResponseDto> UpdatePolicyAsync(
    int policyId,
    UpdatePolicyRequestDto request)
        {
            // Check Customer Exists
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);

            if (customer == null)
            {
                throw new NotFoundException(
                    $"Customer with ID {request.CustomerId} was not found.");
            }

            // Check Company Exists
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {request.CompanyId} was not found.");
            }

            // Check Plan Exists
            var plan = await _planRepository.GetByIdAsync(request.PlanId);

            if (plan == null)
            {
                throw new NotFoundException(
                    $"Insurance Plan with ID {request.PlanId} was not found.");
            }

            // Check Plan belongs to Company
            if (plan.CompanyId != request.CompanyId)
            {
                throw new BadRequestException(
                    "The selected Insurance Plan does not belong to the selected Insurance Company.");
            }

            // Duplicate Policy Number
            if (await _repository.ExistsByPolicyNumberAsync(
                request.PolicyNumber,
                policyId))
            {
                throw new ConflictException(
                    $"Policy Number '{request.PolicyNumber}' already exists.");
            }

            // Premium Validation
            if (request.PremiumAmount <= 0)
            {
                throw new BadRequestException(
                    "Premium Amount must be greater than zero.");
            }

            // Sum Assured Validation
            if (request.SumAssured < plan.MinimumSumAssured ||
                request.SumAssured > plan.MaximumSumAssured)
            {
                throw new BadRequestException(
                    $"Sum Assured must be between {plan.MinimumSumAssured} and {plan.MaximumSumAssured}.");
            }

            // Date Validation
            if (request.StartDate > request.EndDate)
            {
                throw new BadRequestException(
                    "Start Date cannot be later than End Date.");
            }

            var policy = _mapper.Map<Policy>(request);

            var updatedPolicy = await _repository.UpdateAsync(policyId, policy);

            if (updatedPolicy == null)
            {
                throw new NotFoundException(
                    $"Policy with ID {policyId} was not found.");
            }

            return _mapper.Map<PolicyResponseDto>(updatedPolicy);
        }

        public async Task<bool> DeletePolicyAsync(int policyId)
        {
            var deleted = await _repository.DeleteAsync(policyId);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Policy with ID {policyId} was not found.");
            }

            return true;
        }

        public async Task<IEnumerable<PolicyResponseDto>> SearchAsync(string keyword)
        {
            var policies = await _repository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }
    }
}