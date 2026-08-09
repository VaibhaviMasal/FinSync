using FinSync.Application.Features.AuditLogs.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly FinSyncDbContext _context;

        public AuditLogRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetRecentAsync()
        {
            return await _context.AuditLogs
                .OrderByDescending(x => x.PerformedAt)
                .Take(20)
                .ToListAsync();
        }
    }
}