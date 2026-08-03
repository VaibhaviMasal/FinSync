using FinSync.Application.Features.Dashboard.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            return Ok(summary);
        }

        [HttpGet("alerts")]
        public async Task<IActionResult> GetDashboardAlerts()
        {
            var result = await _dashboardService.GetDashboardAlertsAsync();
            return Ok(result);
        }
    }
}