using FinSync.Application.Features.Authentication.DTOs;
using FinSync.Application.Features.Authentication.Interfaces;
using FinSync.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace FinSync.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequestDto request)
        {
            var result = await _authenticationService.RegisterAsync(request);

            return Ok(ApiResponseFactory.Success(
                result,
                "User registered successfully."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authenticationService.LoginAsync(request);

            return Ok(ApiResponseFactory.Success(
                result,
                "Login successful."));
        }
    }
}