using FinSync.Application.Features.AuditLogs.DTOs;

namespace FinSync.Application.Features.AuditLogs.Interfaces
{
    public interface IAuditLogService
    {
        Task AddAsync(string action, string entityName, int entityId, string performedBy);

        Task<List<AuditLogResponseDto>> GetRecentAsync();
    }
}