using FinSync.Domain.Enums;

namespace FinSync.Application.Features.Policies.DTOs
{
    public class PolicyResponseDto
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public int PlanId { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public DateTime IssueDate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal PremiumAmount { get; set; }

        public PremiumFrequency PremiumFrequency { get; set; }

        public decimal SumAssured { get; set; }

        public string PolicyStatus { get; set; } = string.Empty;

        public string NomineeName { get; set; } = string.Empty;

        public string NomineeRelation { get; set; } = string.Empty;

        public string NomineePhoneNumber { get; set; } = string.Empty;
    }
}