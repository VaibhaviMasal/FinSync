using FinSync.Application.Features.Documents.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly FinSyncDbContext _context;

        public DocumentRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<Document> UploadAsync(Document document)
        {
            _context.Documents.Add(document);

            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<IEnumerable<Document>> GetByPolicyIdAsync(int policyId)
        {
            return await _context.Documents
                .Where(d => d.PolicyId == policyId)
                .OrderByDescending(d => d.UploadedDate)
                .ToListAsync();
        }

        public async Task<Document?> GetByIdAsync(int documentId)
        {
            return await _context.Documents
                .FirstOrDefaultAsync(d => d.DocumentId == documentId);
        }

        public async Task DeleteAsync(Document document)
        {
            _context.Documents.Remove(document);

            await _context.SaveChangesAsync();
        }
    }
}