namespace FinSync.Application.Features.Policies.DTOs
{
    public class UpdatePolicyRequestDto
    {
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

        public string PolicyStatus { get; set; } = string.Empty;

        public string NomineeName { get; set; } = string.Empty;

        public string NomineeRelation { get; set; } = string.Empty;

        public string NomineePhoneNumber { get; set; } = string.Empty;
    }
}