namespace FinSync.Application.Features.InsurancePlans.DTOs;

public class UpdateInsurancePlanRequestDto
{
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

    public bool IsActive { get; set; }
}