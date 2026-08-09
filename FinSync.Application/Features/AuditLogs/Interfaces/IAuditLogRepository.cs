using FinSync.Domain.Entities;

namespace FinSync.Application.Features.AuditLogs.Interfaces
{
    public interface IAuditLogRepository
    {
        Task AddAsync(AuditLog log);

        Task<List<AuditLog>> GetRecentAsync();
    }
}