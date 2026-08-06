using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Documents.Interfaces
{
    public interface IDocumentRepository
    {
        Task<Document> UploadAsync(Document document);

        Task<IEnumerable<Document>> GetByPolicyIdAsync(int policyId);

        Task<Document?> GetByIdAsync(int documentId);

        Task DeleteAsync(Document document);
    }
}