using FinSync.Application.Features.Dashboard.DTOs;
using FinSync.Application.Features.Dashboard.Interfaces;

namespace FinSync.Application.Features.Dashboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardSummaryDto> GetDashboardSummaryAsync()
        {
            return await _dashboardRepository.GetDashboardSummaryAsync();
        }

        public async Task<DashboardRecentActivityDto> GetRecentActivityAsync()
        {
            return await _dashboardRepository.GetRecentActivityAsync();
        }

        public async Task<DashboardAnalyticsDto> GetDashboardAnalyticsAsync()
        {
            return await _dashboardRepository.GetDashboardAnalyticsAsync();
        }
        public async Task<IEnumerable<DashboardAlertDto>> GetDashboardAlertsAsync()
        {
            return await _dashboardRepository.GetDashboardAlertsAsync();
        }
    }
}