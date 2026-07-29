using AutoMapper;
using FinSync.Application.Features.Agents.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Agents.Mapping
{
    public class AgentMappingProfile : Profile
    {
        public AgentMappingProfile()
        {
            CreateMap<CreateAgentRequestDto, Agent>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.JoiningDate,
                    opt => opt.MapFrom(src => src.JoiningDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<UpdateAgentRequestDto, Agent>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime(TimeOnly.MinValue)))
                .ForMember(dest => dest.JoiningDate,
                    opt => opt.MapFrom(src => src.JoiningDate.ToDateTime(TimeOnly.MinValue)));

            CreateMap<Agent, AgentResponseDto>()
                .ForMember(dest => dest.DateOfBirth,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateOfBirth)))
                .ForMember(dest => dest.JoiningDate,
                    opt => opt.MapFrom(src => DateOnly.FromDateTime(src.JoiningDate)));
        }
    }
}