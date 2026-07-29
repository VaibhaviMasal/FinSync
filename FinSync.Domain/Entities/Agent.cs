using FinSync.Domain.Enums;

namespace FinSync.Domain.Entities
{
    public class Agent
    {
        public int AgentId { get; set; }

        // Personal Details
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Contact Details
        public string MobileNumber { get; set; } = string.Empty;

        public string? AlternateMobileNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        // KYC Details
        public string PanNumber { get; set; } = string.Empty;

        public string AadhaarNumber { get; set; } = string.Empty;

        // Professional Details
        public string LicenseNumber { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; }

        // Address
        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;

        // Status
        public bool IsActive { get; set; } = true;

        // Audit Fields
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Property
        public ICollection<Policy> Policies { get; set; }
            = new List<Policy>();
    }
}