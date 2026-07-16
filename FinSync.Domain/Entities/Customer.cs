using FinSync.Domain.Enums;

namespace FinSync.Domain.Entities;

public class Customer
{
    public int CustomerId { get; set; }

    // Personal Information
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MobileNumber { get; set; }
    public required string Email { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required Gender Gender { get; set; }
    

    // KYC Details
    public required string PanNumber { get; set; }
    public required string AadhaarNumber { get; set; }


    // Professional Details
    public string? Occupation { get; set; }
    public string? CompanyName { get; set; }
    public decimal? AnnualIncome { get; set; }


    // Contact Details
    public string? AlternateMobileNumber { get; set; }


    // Address
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Pincode { get; set; }


    // Additional Information
    public string? Remarks { get; set; }


    // Audit
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }
}