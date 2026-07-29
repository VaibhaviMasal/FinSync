using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class InsurancePlanConfiguration : IEntityTypeConfiguration<InsurancePlan>
    {
        public void Configure(EntityTypeBuilder<InsurancePlan> builder)
        {
            builder.ToTable("InsurancePlans");

            builder.HasKey(x => x.PlanId);

            builder.Property(x => x.PlanName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.PlanCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(x => x.PlanCode)
                .IsUnique();

            builder.Property(x => x.PlanType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.PremiumFrequency)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.MinimumSumAssured)
                .HasPrecision(18, 2);

            builder.Property(x => x.MaximumSumAssured)
                .HasPrecision(18, 2);

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.UpdatedDate);

            // Relationship
            builder.HasOne(x => x.InsuranceCompany)
                .WithMany(x => x.InsurancePlans)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}