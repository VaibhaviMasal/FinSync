namespace FinSync.Domain.Entities
{
    public class InsuranceCompany
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyCode { get; set; } = string.Empty;

        public string ContactPerson { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Website { get; set; } = string.Empty;

        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public ICollection<InsurancePlan> InsurancePlans { get; set; }
    = new List<InsurancePlan>();

        public ICollection<Policy> Policies { get; set; } = new List<Policy>();
    }
}