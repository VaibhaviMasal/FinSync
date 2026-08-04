using AutoMapper;
using FinSync.Application.Features.PremiumPayments.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.PremiumPayments.Mapping
{
    public class PremiumPaymentMappingProfile : Profile
    {
        public PremiumPaymentMappingProfile()
        {
            CreateMap<CreatePremiumPaymentRequestDto, PremiumPayment>();

            CreateMap<UpdatePremiumPaymentRequestDto, PremiumPayment>();

            CreateMap<PremiumPayment, PremiumPaymentResponseDto>();
        }
    }
}