using AutoMapper;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Application.Features.InsurancePlans.DTOs;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.InsurancePlans.Services
{
    public class InsurancePlanService : IInsurancePlanService
    {
        private readonly IInsurancePlanRepository _repository;
        private readonly IInsuranceCompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public InsurancePlanService(
            IInsurancePlanRepository repository,
            IInsuranceCompanyRepository companyRepository,
            IMapper mapper)
        {
            _repository = repository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        // Create
        public async Task<InsurancePlanResponseDto> CreatePlanAsync(
            CreateInsurancePlanRequestDto request)
        {
            // Check Company Exists
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {request.CompanyId} was not found.");
            }

            // Duplicate Plan Code
            if (await _repository.ExistsByPlanCodeAsync(request.PlanCode))
            {
                throw new ConflictException(
                    $"Plan Code '{request.PlanCode}' already exists.");
            }

            // Duplicate Plan Name within same company
            if (await _repository.ExistsByPlanNameAsync(
                request.PlanName,
                request.CompanyId))
            {
                throw new ConflictException(
                    $"Plan '{request.PlanName}' already exists for this company.");
            }

            // Age Validation
            if (request.MinimumAge > request.MaximumAge)
            {
                throw new BadRequestException(
                    "Minimum Age cannot be greater than Maximum Age.");
            }

            // Sum Assured Validation
            if (request.MinimumSumAssured > request.MaximumSumAssured)
            {
                throw new BadRequestException(
                    "Minimum Sum Assured cannot be greater than Maximum Sum Assured.");
            }

            var insurancePlan = _mapper.Map<InsurancePlan>(request);

            var createdPlan = await _repository.AddAsync(insurancePlan);

            return _mapper.Map<InsurancePlanResponseDto>(createdPlan);
        }

        // Get All
        public async Task<IEnumerable<InsurancePlanResponseDto>> GetAllAsync(
            InsurancePlanQueryParametersDto queryParameters)
        {
            var plans = await _repository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<InsurancePlanResponseDto>>(plans);
        }

        // Get By Id
        public async Task<InsurancePlanResponseDto> GetByIdAsync(int planId)
        {
            var plan = await _repository.GetByIdAsync(planId);

            if (plan == null)
            {
                throw new NotFoundException(
                    $"Insurance Plan with ID {planId} was not found.");
            }

            return _mapper.Map<InsurancePlanResponseDto>(plan);
        }

        // Update
        public async Task<InsurancePlanResponseDto> UpdatePlanAsync(
            int planId,
            UpdateInsurancePlanRequestDto request)
        {
            // Check Company Exists
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {request.CompanyId} was not found.");
            }

            // Duplicate Plan Code
            if (await _repository.ExistsByPlanCodeAsync(
                request.PlanCode,
                planId))
            {
                throw new ConflictException(
                    $"Plan Code '{request.PlanCode}' already exists.");
            }

            // Duplicate Plan Name
            if (await _repository.ExistsByPlanNameAsync(
                request.PlanName,
                request.CompanyId,
                planId))
            {
                throw new ConflictException(
                    $"Plan '{request.PlanName}' already exists for this company.");
            }

            // Age Validation
            if (request.MinimumAge > request.MaximumAge)
            {
                throw new BadRequestException(
                    "Minimum Age cannot be greater than Maximum Age.");
            }

            // Sum Assured Validation
            if (request.MinimumSumAssured > request.MaximumSumAssured)
            {
                throw new BadRequestException(
                    "Minimum Sum Assured cannot be greater than Maximum Sum Assured.");
            }

            var insurancePlan = _mapper.Map<InsurancePlan>(request);

            var updatedPlan = await _repository.UpdateAsync(
                planId,
                insurancePlan);

            if (updatedPlan == null)
            {
                throw new NotFoundException(
                    $"Insurance Plan with ID {planId} was not found.");
            }

            return _mapper.Map<InsurancePlanResponseDto>(updatedPlan);
        }

        // Delete
        public async Task<bool> DeletePlanAsync(int planId)
        {
            var deleted = await _repository.DeleteAsync(planId);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Insurance Plan with ID {planId} was not found.");
            }

            return true;
        }

        // Search
        public async Task<IEnumerable<InsurancePlanResponseDto>> SearchAsync(
            string keyword)
        {
            var plans = await _repository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<InsurancePlanResponseDto>>(plans);
        }
    }
}