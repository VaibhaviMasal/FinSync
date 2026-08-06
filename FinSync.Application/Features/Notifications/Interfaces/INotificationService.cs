using FinSync.Application.Features.Notifications.DTOs;

namespace FinSync.Application.Features.Notifications.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationResponseDto> CreateAsync(CreateNotificationRequestDto request);

        Task<IEnumerable<NotificationResponseDto>> GetAllAsync();

        Task<IEnumerable<NotificationResponseDto>> GetUnreadAsync();

        Task MarkAsReadAsync(int notificationId);
    }
}