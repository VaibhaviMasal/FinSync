using AutoMapper;
using FinSync.Application.Features.Notifications.DTOs;
using FinSync.Application.Features.Notifications.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Shared.Exceptions;

namespace FinSync.Application.Features.Notifications.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<NotificationResponseDto> CreateAsync(CreateNotificationRequestDto request)
        {
            var notification = _mapper.Map<Notification>(request);

            var created = await _repository.AddAsync(notification);

            return _mapper.Map<NotificationResponseDto>(created);
        }

        public async Task<IEnumerable<NotificationResponseDto>> GetAllAsync()
        {
            var notifications = await _repository.GetAllAsync();

            return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
        }

        public async Task<IEnumerable<NotificationResponseDto>> GetUnreadAsync()
        {
            var notifications = await _repository.GetUnreadAsync();

            return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _repository.GetByIdAsync(notificationId);

            if (notification == null)
                throw new NotFoundException("Notification not found.");

            await _repository.MarkAsReadAsync(notification);
        }
    }
}