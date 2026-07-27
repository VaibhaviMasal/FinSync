using System.ComponentModel.DataAnnotations.Schema;

namespace FinSync.Domain.Entities
{
    public class InsurancePlan
    {
        public int PlanId { get; set; }

        public int CompanyId { get; set; }

        public string PlanName { get; set; } = string.Empty;

        public string PlanCode { get; set; } = string.Empty;

        public string PlanType { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int MinimumAge { get; set; }

        public int MaximumAge { get; set; }

        public int PolicyTerm { get; set; }

        public string PremiumFrequency { get; set; } = string.Empty;

        public decimal MinimumSumAssured { get; set; }

        public decimal MaximumSumAssured { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        // Navigation Property
        public InsuranceCompany InsuranceCompany { get; set; } = null!;

        public ICollection<Policy> Policies { get; set; } = new List<Policy>();

    }
}