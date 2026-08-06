using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Documents.DTOs
{
    public class DocumentResponseDto
    {
        public int DocumentId { get; set; }

        public int PolicyId { get; set; }

        public string DocumentType { get; set; } = string.Empty;

        public string OriginalFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedDate { get; set; }

        public string? Remarks { get; set; }
    }
}