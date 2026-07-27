using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class PolicyConfiguration : IEntityTypeConfiguration<Policy>
    {
        public void Configure(EntityTypeBuilder<Policy> builder)
        {
            builder.ToTable("Policies");

            builder.HasKey(x => x.PolicyId);

            builder.Property(x => x.PolicyNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(x => x.PolicyNumber)
                .IsUnique();

            builder.Property(x => x.PremiumAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.SumAssured)
                .HasPrecision(18, 2);

            builder.Property(x => x.PremiumFrequency)
                .HasMaxLength(20);

            builder.Property(x => x.PolicyStatus)
                .HasMaxLength(20);

            builder.Property(x => x.NomineeName)
                .HasMaxLength(100);

            builder.Property(x => x.NomineeRelation)
                .HasMaxLength(50);

            builder.Property(x => x.NomineePhoneNumber)
                .HasMaxLength(10);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Policies)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.InsuranceCompany)
                .WithMany(x => x.Policies)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.InsurancePlan)
                .WithMany(x => x.Policies)
                .HasForeignKey(x => x.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}