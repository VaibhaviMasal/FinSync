using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class InsuranceCompanyConfiguration : IEntityTypeConfiguration<InsuranceCompany>
    {
        public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
        {
            builder.ToTable("InsuranceCompanies");

            builder.HasKey(x => x.CompanyId);

            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.CompanyCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.CompanyCode)
                .IsUnique();

            builder.Property(x => x.ContactPerson)
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .HasMaxLength(150);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(x => x.Website)
                .HasMaxLength(250);

            builder.Property(x => x.AddressLine1)
    .IsRequired()
    .HasMaxLength(200);

            builder.Property(x => x.AddressLine2)
                .HasMaxLength(200);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Pincode)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.UpdatedDate);
        }
    }
}