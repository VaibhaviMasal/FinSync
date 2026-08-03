using FinSync.Domain.Enums;

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


        public PremiumFrequency PremiumFrequency { get; set; }

        public decimal SumAssured { get; set; }

        public string PolicyStatus { get; set; } = "Active";

        public string NomineeName { get; set; } = string.Empty;

        public string NomineeRelation { get; set; } = string.Empty;

        public string NomineePhoneNumber { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public int AgentId { get; set; } 

        // Navigation Properties
        public Customer Customer { get; set; } = null!;

        public InsuranceCompany InsuranceCompany { get; set; } = null!;

        public InsurancePlan InsurancePlan { get; set; } = null!;

        public Agent Agent { get; set; } = null!;


        public ICollection<PremiumPayment> PremiumPayments { get; set; }
    = new List<PremiumPayment>();

        public ICollection<PolicyRenewal> PolicyRenewals { get; set; }
    = new List<PolicyRenewal>();

        public ICollection<InsuranceClaim> InsuranceClaims { get; set; }
    = new List<InsuranceClaim>();

    }
}