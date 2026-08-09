namespace FinSync.Domain.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        public string Action { get; set; } = string.Empty;
        // e.g., "Customer Created"

        public string EntityName { get; set; } = string.Empty;
        // e.g., "Customer"

        public int EntityId { get; set; }

        public string PerformedBy { get; set; } = string.Empty;

        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

        public string? Remarks { get; set; }
    }
}