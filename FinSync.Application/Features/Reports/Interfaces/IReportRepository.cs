using FinSync.Application.Features.Reports.DTOs;

namespace FinSync.Application.Features.Reports.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<ActivePolicyReportDto>> GetActivePoliciesAsync(ReportFilterDto filter);

        Task<IEnumerable<ExpiringPolicyReportDto>> GetExpiringPoliciesAsync(ReportFilterDto filter);

        Task<PremiumCollectionReportDto> GetPremiumCollectionAsync(ReportFilterDto filter);

        Task<IEnumerable<AgentPerformanceReportDto>> GetAgentPerformanceAsync(ReportFilterDto filter);

        Task<IEnumerable<CompanyBusinessReportDto>> GetCompanyBusinessAsync(ReportFilterDto filter);

        Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(ReportFilterDto filter);

        Task<DashboardReportDto> GetDashboardReportAsync();
    }
}