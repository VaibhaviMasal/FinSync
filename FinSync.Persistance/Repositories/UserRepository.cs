using FinSync.Application.Features.Authentication.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinSyncDbContext _context;

        public UserRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task AddAsync(ApplicationUser user)
        {
            await _context.ApplicationUsers.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}