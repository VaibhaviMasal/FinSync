using AutoMapper;
using FinSync.Application.Features.Documents.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Documents.Mapping
{
    public class DocumentMappingProfile : Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<Document, DocumentResponseDto>();
        }
    }
}