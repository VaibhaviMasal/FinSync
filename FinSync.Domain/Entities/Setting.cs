namespace FinSync.Domain.Entities
{
    public class Setting
    {
        public int SettingId { get; set; }

        public string BusinessName { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Currency { get; set; } = "INR";

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }
    }
}