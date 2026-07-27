namespace FinSync.Domain.Entities
{
    public class Policy
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public int CompanyId { get; set; }

        public int PlanId { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PremiumAmount { get; set; }

        public string PremiumFrequency { get; set; } = string.Empty;

        public decimal SumAssured { get; set; }

        public string PolicyStatus { get; set; } = "Active";

        public string NomineeName { get; set; } = string.Empty;

        public string NomineeRelation { get; set; } = string.Empty;

        public string NomineePhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Properties
        public Customer Customer { get; set; } = null!;

        public InsuranceCompany InsuranceCompany { get; set; } = null!;

        public InsurancePlan InsurancePlan { get; set; } = null!;
    }
}