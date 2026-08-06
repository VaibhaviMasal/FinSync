using AutoMapper;
using FinSync.Application.Features.Notifications.DTOs;
using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Notifications.Mapping
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<CreateNotificationRequestDto, Notification>();

            CreateMap<Notification, NotificationResponseDto>();
        }
    }
}