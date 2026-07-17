namespace FinSync.Application.Features.Customers.DTOs
{
    public class CustomerQueryParametersDto
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string SortBy { get; set; } = "CustomerId";

        public string SortOrder { get; set; } = "asc";
    }
}