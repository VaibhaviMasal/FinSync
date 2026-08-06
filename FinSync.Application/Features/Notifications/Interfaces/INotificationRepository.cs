using FinSync.Domain.Entities;

namespace FinSync.Application.Features.Notifications.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> AddAsync(Notification notification);

        Task<IEnumerable<Notification>> GetAllAsync();

        Task<IEnumerable<Notification>> GetUnreadAsync();

        Task<Notification?> GetByIdAsync(int notificationId);

        Task MarkAsReadAsync(Notification notification);
    }
}