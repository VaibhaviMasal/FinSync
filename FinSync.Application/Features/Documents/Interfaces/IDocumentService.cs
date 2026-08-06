using FinSync.Application.Features.Documents.DTOs;

namespace FinSync.Application.Features.Documents.Interfaces
{
    public interface IDocumentService
    {
        Task<DocumentResponseDto> UploadAsync(CreateDocumentRequestDto request);

        Task<IEnumerable<DocumentResponseDto>> GetByPolicyIdAsync(int policyId);

        Task<DocumentResponseDto?> GetByIdAsync(int documentId);

        Task DeleteAsync(int documentId);
    }
}