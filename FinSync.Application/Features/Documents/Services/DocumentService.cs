using AutoMapper;
using FinSync.Application.Features.Documents.DTOs;
using FinSync.Application.Features.Documents.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Documents.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _repository;
        private readonly IMapper _mapper;

        public DocumentService(
            IDocumentRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DocumentResponseDto> UploadAsync(CreateDocumentRequestDto request)
        {
            var document = new Document
            {
                PolicyId = request.PolicyId,
                DocumentType = request.DocumentType,
                OriginalFileName = request.OriginalFileName,
                StoredFileName = request.OriginalFileName,
                FilePath = string.Empty,
                ContentType = request.ContentType,
                FileSize = request.FileBytes.Length,
                Remarks = request.Remarks
            };

            var uploaded = await _repository.UploadAsync(document);

            return _mapper.Map<DocumentResponseDto>(uploaded);
        }

        public async Task<IEnumerable<DocumentResponseDto>> GetByPolicyIdAsync(int policyId)
        {
            var documents = await _repository.GetByPolicyIdAsync(policyId);

            return _mapper.Map<IEnumerable<DocumentResponseDto>>(documents);
        }

        public async Task<DocumentResponseDto?> GetByIdAsync(int documentId)
        {
            var document = await _repository.GetByIdAsync(documentId);

            if (document == null)
                throw new NotFoundException("Document not found.");

            return _mapper.Map<DocumentResponseDto>(document);
        }

        public async Task DeleteAsync(int documentId)
        {
            var document = await _repository.GetByIdAsync(documentId);

            if (document == null)
                throw new NotFoundException("Document not found.");

            await _repository.DeleteAsync(document);
        }
    }
}