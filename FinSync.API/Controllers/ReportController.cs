using FinSync.Application.Features.Reports.DTOs;
using FinSync.Application.Features.Reports.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _reportService.GetDashboardReportAsync();
            return Ok(result);
        }

        [HttpGet("active-policies")]
        public async Task<IActionResult> GetActivePolicies([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetActivePoliciesAsync(filter);
            return Ok(result);
        }

        [HttpGet("expiring-policies")]
        public async Task<IActionResult> GetExpiringPolicies([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetExpiringPoliciesAsync(filter);
            return Ok(result);
        }

        [HttpGet("premium-collection")]
        public async Task<IActionResult> GetPremiumCollection([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetPremiumCollectionAsync(filter);
            return Ok(result);
        }

        [HttpGet("agent-performance")]
        public async Task<IActionResult> GetAgentPerformance([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetAgentPerformanceAsync(filter);
            return Ok(result);
        }

        [HttpGet("company-business")]
        public async Task<IActionResult> GetCompanyBusiness([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetCompanyBusinessAsync(filter);
            return Ok(result);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims([FromQuery] ReportFilterDto filter)
        {
            var result = await _reportService.GetClaimReportAsync(filter);
            return Ok(result);
        }
    }
}