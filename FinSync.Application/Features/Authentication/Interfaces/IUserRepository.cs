using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Authentication.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task AddAsync(ApplicationUser user);

        Task SaveChangesAsync();
    }
}