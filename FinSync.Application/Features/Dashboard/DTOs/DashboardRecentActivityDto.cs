namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class DashboardRecentActivityDto
    {
        public List<RecentCustomerDto> RecentCustomers { get; set; } = new();

        public List<RecentPolicyDto> RecentPolicies { get; set; } = new();

        public List<RecentPaymentDto> RecentPayments { get; set; } = new();
    }
}