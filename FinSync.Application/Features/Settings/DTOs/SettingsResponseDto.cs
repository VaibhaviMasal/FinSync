namespace FinSync.Application.Features.Settings.DTOs
{
    public class SettingsResponseDto
    {
        public int SettingId { get; set; }

        public string BusinessName { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;
    }
}