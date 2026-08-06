using FinSync.Application.Features.Notifications.Interfaces;
using FinSync.Domain.Entities;
using FinSync.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly FinSyncDbContext _context;

        public NotificationRepository(FinSyncDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);

            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notifications
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnreadAsync()
        {
            return await _context.Notifications
                .Where(n => !n.IsRead)
                .OrderByDescending(n => n.CreatedDate)
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(int notificationId)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId);
        }

        public async Task MarkAsReadAsync(Notification notification)
        {
            notification.IsRead = true;
            notification.ReadDate = DateTime.UtcNow;

            _context.Notifications.Update(notification);

            await _context.SaveChangesAsync();
        }
    }
}