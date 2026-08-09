namespace FinSync.Application.Features.AuditLogs.DTOs
{
    public class AuditLogResponseDto
    {
        public string Action { get; set; } = string.Empty;

        public string EntityName { get; set; } = string.Empty;

        public int EntityId { get; set; }

        public string PerformedBy { get; set; } = string.Empty;

        public DateTime PerformedAt { get; set; }

        public string? Remarks { get; set; }
    }
}