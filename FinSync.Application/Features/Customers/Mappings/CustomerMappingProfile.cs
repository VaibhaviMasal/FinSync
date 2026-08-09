using AutoMapper;
using FinSync.Application.Features.Customers.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Customers.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // ✅ Create Customer
            CreateMap<CreateCustomerRequestDto, Customer>()
    .ForMember(dest => dest.DateOfBirth,
        opt => opt.MapFrom(src =>
            new DateTime(src.DateOfBirth.Year, src.DateOfBirth.Month, src.DateOfBirth.Day)))
    .ForMember(dest => dest.CreatedDate,
        opt => opt.MapFrom(_ => DateTime.UtcNow))
    .ForMember(dest => dest.IsActive,
        opt => opt.MapFrom(_ => true))
    .ForMember(dest => dest.UpdatedDate,
        opt => opt.Ignore());

            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src =>
                        DateOnly.FromDateTime(src.DateOfBirth)));

            CreateMap<UpdateCustomerRequestDto, Customer>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src =>
                        new DateTime(src.DateOfBirth.Year, src.DateOfBirth.Month, src.DateOfBirth.Day)))
                .ForMember(dest => dest.UpdatedDate,
                    opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}