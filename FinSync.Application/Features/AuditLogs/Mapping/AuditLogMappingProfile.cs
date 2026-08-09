
using AutoMapper;
using FinSync.Application.Features.AuditLogs.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.AuditLogs.Mapping
{
    public class AuditLogMappingProfile : Profile
    {
        public AuditLogMappingProfile()
        {
            CreateMap<AuditLog, AuditLogResponseDto>();
        }
    }
}