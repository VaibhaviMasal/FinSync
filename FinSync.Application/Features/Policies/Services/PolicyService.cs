using AutoMapper;
using FinSync.Application.Features.Customers.Interfaces;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Application.Features.InsurancePlans.Interfaces;
using FinSync.Application.Features.Policies.DTOs;
using FinSync.Application.Features.Policies.Interfaces;

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

        public Task<PolicyResponseDto> CreatePolicyAsync(CreatePolicyRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PolicyResponseDto>> GetAllAsync(PolicyQueryParametersDto queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResponseDto> GetByIdAsync(int policyId)
        {
            throw new NotImplementedException();
        }

        public Task<PolicyResponseDto> UpdatePolicyAsync(int policyId, UpdatePolicyRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePolicyAsync(int policyId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PolicyResponseDto>> SearchAsync(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}