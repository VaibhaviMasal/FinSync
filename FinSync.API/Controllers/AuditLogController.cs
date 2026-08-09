using FinSync.Application.Features.AuditLogs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _service;

        public AuditLogController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecent()
        {
            var result = await _service.GetRecentAsync();

            return Ok(result);
        }
    }
}