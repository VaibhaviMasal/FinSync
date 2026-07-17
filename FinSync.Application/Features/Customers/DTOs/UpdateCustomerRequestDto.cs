namespace FinSync.Application.Features.Customers.DTOs
{
    public class UpdateCustomerRequestDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string? MiddleName { get; set; }

        public string LastName { get; set; } = string.Empty;

        public int Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string MobileNumber { get; set; } = string.Empty;

        public string? AlternateMobileNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string State { get; set; } = string.Empty;

        public string Pincode { get; set; } = string.Empty;

        public string PanNumber { get; set; } = string.Empty;

        public string AadhaarNumber { get; set; } = string.Empty;

        public string Occupation { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public decimal AnnualIncome { get; set; } 

        public string? Remarks { get; set; } = string.Empty;





            
    }
}
