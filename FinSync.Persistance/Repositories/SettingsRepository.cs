using FinSync.Application.Features.Settings.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly FinSyncDbContext _context;

        public SettingsRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<Setting?> GetAsync()
        {
            return await _context.Settings.FirstOrDefaultAsync();
        }

        public async Task<Setting> CreateAsync(Setting setting)
        {
            _context.Settings.Add(setting);

            await _context.SaveChangesAsync();

            return setting;
        }

        public async Task<Setting> UpdateAsync(Setting setting)
        {
            _context.Settings.Update(setting);

            await _context.SaveChangesAsync();

            return setting;
        }
    }
}