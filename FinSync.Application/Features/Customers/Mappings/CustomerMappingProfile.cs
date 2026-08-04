using AutoMapper;
using FinSync.Application.Features.Customers.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Customers.Mappings
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            // Create Customer
            CreateMap<CreateCustomerRequestDto, Customer>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.CreatedDate,
                    opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive,
                    opt => opt.MapFrom(_ => true))
                .ForMember(dest => dest.UpdatedDate,
                    opt => opt.Ignore());

            // Customer Response
            CreateMap<Customer, CustomerResponseDto>()
              .ForMember(dest => dest.DateOfBirth,
               opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)));

            //  UpdateCustomer 
            CreateMap<UpdateCustomerRequestDto, Customer>();
        }
    }
}