namespace FinSync.Application.Features.Authentication.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }
    }
}