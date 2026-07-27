public class PolicyQueryParametersDto
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SearchTerm { get; set; }

    public string? SortBy { get; set; }

    public bool IsDescending { get; set; }

    public int? CustomerId { get; set; }

    public int? CompanyId { get; set; }

    public int? PlanId { get; set; }
}