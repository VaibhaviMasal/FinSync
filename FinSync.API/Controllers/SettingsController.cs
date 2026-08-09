using FinSync.Application.Features.Settings.DTOs;
using FinSync.Application.Features.Settings.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _settingsService.GetAsync();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSettingsRequestDto request)
        {
            var result = await _settingsService.UpdateAsync(request);

            return Ok(result);
        }
    }
}