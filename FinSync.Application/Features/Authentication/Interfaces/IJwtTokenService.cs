using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Authentication.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}