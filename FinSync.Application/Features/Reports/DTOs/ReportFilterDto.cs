namespace FinSync.Application.Features.Reports.DTOs
{
    public class ReportFilterDto
    {
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public int? CompanyId { get; set; }

        public int? AgentId { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "PolicyNumber";

        public string SortOrder { get; set; } = "asc";
    }
}