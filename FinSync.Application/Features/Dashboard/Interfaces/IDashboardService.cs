using FinSync.Application.Features.Dashboard.DTOs;

namespace FinSync.Application.Features.Dashboard.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();

        Task<IEnumerable<DashboardAlertDto>> GetDashboardAlertsAsync();

        Task<DashboardRecentActivityDto> GetRecentActivityAsync();

        Task<DashboardAnalyticsDto> GetDashboardAnalyticsAsync();
    }
}