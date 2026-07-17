
using FinSync.Domain.Enums;

namespace FinSync.Application.DTOs.Customer
{
    public class CreateCustomerRequestDto
    {
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }

        public required Gender Gender { get; set; }
        public required DateOnly DateOfBirth { get; set; }

        public required string MobileNumber { get; set; }
        public string? AlternateMobileNumber { get; set; }
        public required string Email { get; set; }

        public required string Address { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Pincode { get; set; }

        public required string PanNumber { get; set; }
        public required string AadhaarNumber { get; set; }

        public string? Occupation { get; set; }
        public string? CompanyName { get; set; }
        public decimal? AnnualIncome { get; set; }

        public string? Remarks { get; set; }
    }
}
