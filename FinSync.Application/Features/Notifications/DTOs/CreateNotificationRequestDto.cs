namespace FinSync.Application.Features.Notifications.DTOs
{
    public class CreateNotificationRequestDto
    {
        public string Title { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public string NotificationType { get; set; } = string.Empty;
    }
}