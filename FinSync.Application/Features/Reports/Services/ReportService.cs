using FinSync.Application.Features.Reports.DTOs;
using FinSync.Application.Features.Reports.Interfaces;

namespace FinSync.Application.Features.Reports.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;

        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<IEnumerable<ActivePolicyReportDto>> GetActivePoliciesAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetActivePoliciesAsync(filter);
        }

        public async Task<IEnumerable<ExpiringPolicyReportDto>> GetExpiringPoliciesAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetExpiringPoliciesAsync(filter);
        }

        public async Task<PremiumCollectionReportDto> GetPremiumCollectionAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetPremiumCollectionAsync(filter);
        }

        public async Task<IEnumerable<AgentPerformanceReportDto>> GetAgentPerformanceAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetAgentPerformanceAsync(filter);
        }

        public async Task<IEnumerable<CompanyBusinessReportDto>> GetCompanyBusinessAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetCompanyBusinessAsync(filter);
        }

        public async Task<IEnumerable<ClaimReportDto>> GetClaimReportAsync(ReportFilterDto filter)
        {
            return await _reportRepository.GetClaimReportAsync(filter);
        }

        public async Task<DashboardReportDto> GetDashboardReportAsync()
        {
            return await _reportRepository.GetDashboardReportAsync();
        }
    }
}