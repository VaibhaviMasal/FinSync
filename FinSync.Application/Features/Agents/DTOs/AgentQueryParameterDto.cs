namespace FinSync.Application.Features.Agents.DTOs
{
    public class AgentQueryParametersDto
    {
        public string? SearchTerm { get; set; }

        public bool? IsActive { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "FirstName";

        public bool Descending { get; set; } = false;
    }
}