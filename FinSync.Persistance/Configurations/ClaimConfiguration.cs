using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class ClaimConfiguration : IEntityTypeConfiguration<InsuranceClaim>
    {
        public void Configure(EntityTypeBuilder<InsuranceClaim> builder)
        {
            builder.ToTable("Claims");

            builder.HasKey(x => x.ClaimId);

            builder.Property(x => x.ClaimNumber)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasIndex(x => x.ClaimNumber)
                .IsUnique();

            builder.Property(x => x.ClaimAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Remarks)
                .HasMaxLength(500);

            builder.Property(x => x.ClaimStatus)
                .HasConversion<string>();

            builder.Property(x => x.ClaimType)
                .HasConversion<string>();

            builder.HasOne(x => x.Policy)
                .WithMany(x => x.InsuranceClaims)
                .HasForeignKey(x => x.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}