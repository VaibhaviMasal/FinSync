namespace FinSync.Domain.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        
        public int PolicyId { get; set; }

        public string DocumentType { get; set; } = string.Empty;

        public string OriginalFileName { get; set; } = string.Empty;

        public string StoredFileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public string? Remarks { get; set; }

        public DateTime UploadedDate { get; set; } = DateTime.UtcNow;

        public Policy Policy { get; set; } = null!;
    }
}