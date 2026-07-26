using FinSync.Application.Features.InsuranceCompanies.DTOs;
using FinSync.Application.Features.InsuranceCompanies.Interfaces;
using FinSync.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly IInsuranceCompanyService _service;

        public InsuranceCompanyController(IInsuranceCompanyService service)
        {
            _service = service;
        }

        // POST: api/InsuranceCompany
        [HttpPost]
        public async Task<IActionResult> Create(
            CreateInsuranceCompanyRequestDto request)
        {
            var company = await _service.CreateCompanyAsync(request);

            return Ok(ApiResponseFactory.Success(
                company,
                "Insurance Company created successfully."));
        }

        // GET: api/InsuranceCompany
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] InsuranceCompanyQueryParametersDto queryParameters)
        {
            var companies = await _service.GetAllAsync(queryParameters);

            return Ok(ApiResponseFactory.Success(
                companies,
                "Insurance Companies retrieved successfully."));
        }

        // GET: api/InsuranceCompany/5
        [HttpGet("{companyId:int}")]
        public async Task<IActionResult> GetById(int companyId)
        {
            var company = await _service.GetByIdAsync(companyId);

            return Ok(ApiResponseFactory.Success(
                company,
                "Insurance Company retrieved successfully."));
        }

        // PUT: api/InsuranceCompany/5
        [HttpPut("{companyId:int}")]
        public async Task<IActionResult> Update(
            int companyId,
            UpdateInsuranceCompanyRequestDto request)
        {
            var company = await _service.UpdateCompanyAsync(
                companyId,
                request);

            return Ok(ApiResponseFactory.Success(
                company,
                "Insurance Company updated successfully."));
        }

        // DELETE: api/InsuranceCompany/5
        [HttpDelete("{companyId:int}")]
        public async Task<IActionResult> Delete(int companyId)
        {
            await _service.DeleteCompanyAsync(companyId);

            return Ok(ApiResponseFactory.Success(
                true,
                "Insurance Company deleted successfully."));
        }

        // GET: api/InsuranceCompany/search?keyword=LIC
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var companies = await _service.SearchAsync(keyword);

            return Ok(ApiResponseFactory.Success(
                companies,
                "Insurance Companies retrieved successfully."));
        }
    }
}