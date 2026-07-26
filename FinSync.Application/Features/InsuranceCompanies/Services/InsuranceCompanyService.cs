using AutoMapper;
using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.InsuranceCompanies.Services
{
    public class InsuranceCompanyService : IInsuranceCompanyService
    {
        private readonly IInsuranceCompanyRepository _repository;
        private readonly IMapper _mapper;

        public InsuranceCompanyService(
            IInsuranceCompanyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Create
        public async Task<InsuranceCompanyResponseDto> CreateCompanyAsync(
            CreateInsuranceCompanyRequestDto request)
        {
            if (await _repository.ExistsByCompanyCodeAsync(request.CompanyCode))
            {
                throw new ConflictException(
                    $"Company Code '{request.CompanyCode}' already exists.");
            }

            if (await _repository.ExistsByCompanyNameAsync(request.CompanyName))
            {
                throw new ConflictException(
                    $"Company Name '{request.CompanyName}' already exists.");
            }

            var company = _mapper.Map<InsuranceCompany>(request);

            var createdCompany = await _repository.AddAsync(company);

            return _mapper.Map<InsuranceCompanyResponseDto>(createdCompany);
        }

        // Get All
        public async Task<IEnumerable<InsuranceCompanyResponseDto>> GetAllAsync(
            InsuranceCompanyQueryParametersDto queryParameters)
        {
            var companies = await _repository.GetAllAsync(queryParameters);

            return _mapper.Map<IEnumerable<InsuranceCompanyResponseDto>>(companies);
        }

        // Get By Id
        public async Task<InsuranceCompanyResponseDto> GetByIdAsync(int companyId)
        {
            var company = await _repository.GetByIdAsync(companyId);

            if (company == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {companyId} was not found.");
            }

            return _mapper.Map<InsuranceCompanyResponseDto>(company);
        }

        // Update
        public async Task<InsuranceCompanyResponseDto> UpdateCompanyAsync(
            int companyId,
            UpdateInsuranceCompanyRequestDto request)
        {
            if (await _repository.ExistsByCompanyCodeAsync(
                request.CompanyCode,
                companyId))
            {
                throw new ConflictException(
                    $"Company Code '{request.CompanyCode}' already exists.");
            }

            if (await _repository.ExistsByCompanyNameAsync(
                request.CompanyName,
                companyId))
            {
                throw new ConflictException(
                    $"Company Name '{request.CompanyName}' already exists.");
            }

            var company = _mapper.Map<InsuranceCompany>(request);

            var updatedCompany = await _repository.UpdateAsync(
                companyId,
                company);

            if (updatedCompany == null)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {companyId} was not found.");
            }

            return _mapper.Map<InsuranceCompanyResponseDto>(updatedCompany);
        }

        // Delete
        public async Task<bool> DeleteCompanyAsync(int companyId)
        {
            var deleted = await _repository.DeleteAsync(companyId);

            if (!deleted)
            {
                throw new NotFoundException(
                    $"Insurance Company with ID {companyId} was not found.");
            }

            return true;
        }

        // Search
        public async Task<IEnumerable<InsuranceCompanyResponseDto>> SearchAsync(
            string keyword)
        {
            var companies = await _repository.SearchAsync(keyword);

            return _mapper.Map<IEnumerable<InsuranceCompanyResponseDto>>(companies);
        }
    }
}