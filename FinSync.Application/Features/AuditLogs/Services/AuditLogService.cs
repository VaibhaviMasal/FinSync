using AutoMapper;
using FinSync.Application.Features.AuditLogs.DTOs;
using FinSync.Application.Features.AuditLogs.Interfaces;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.AuditLogs.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;
        private readonly IMapper _mapper;

        public AuditLogService(IAuditLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddAsync(string action, string entityName, int entityId, string performedBy)
        {
            var log = new AuditLog
            {
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                PerformedBy = performedBy
            };

            await _repository.AddAsync(log);
        }

        public async Task<List<AuditLogResponseDto>> GetRecentAsync()
        {
            var logs = await _repository.GetRecentAsync();

            return _mapper.Map<List<AuditLogResponseDto>>(logs);
        }
    }
}