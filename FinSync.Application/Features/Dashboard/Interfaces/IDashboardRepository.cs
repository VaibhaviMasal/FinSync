using FinSync.Application.Features.Dashboard.DTOs;

namespace FinSync.Application.Features.Dashboard.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardSummaryDto> GetDashboardSummaryAsync();

        Task<IEnumerable<DashboardAlertDto>> GetDashboardAlertsAsync();

        Task<DashboardRecentActivityDto> GetRecentActivityAsync();

        Task<DashboardAnalyticsDto> GetDashboardAnalyticsAsync();
    }
}