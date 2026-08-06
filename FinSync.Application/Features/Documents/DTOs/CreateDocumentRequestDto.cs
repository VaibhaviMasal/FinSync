namespace FinSync.Application.Features.Documents.DTOs
{
    public class CreateDocumentRequestDto
    {
        public int PolicyId { get; set; }

        public string DocumentType { get; set; } = string.Empty;

        public string OriginalFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public byte[] FileBytes { get; set; } = Array.Empty<byte>();

        public string? Remarks { get; set; }
    }
}