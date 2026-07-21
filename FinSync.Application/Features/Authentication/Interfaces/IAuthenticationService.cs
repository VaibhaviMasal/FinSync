using FinSync.Application.Features.Authentication.DTOs;

namespace FinSync.Application.Features.Authentication.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> RegisterAsync(RegisterUserRequestDto request);

        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}