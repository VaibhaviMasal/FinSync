using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class RenewalConfiguration : IEntityTypeConfiguration<PolicyRenewal>
    {
        public void Configure(EntityTypeBuilder<PolicyRenewal> builder)
        {
            builder.HasKey(x => x.RenewalId);

            builder.Property(x => x.RenewalPremium)
                .HasPrecision(18, 2);

            builder.Property(x => x.RenewalStatus)
                .HasConversion<string>();

            builder.Property(x => x.Remarks)
                .HasMaxLength(500);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.HasOne(x => x.Policy)
                .WithMany(x => x.Renewals)
                .HasForeignKey(x => x.PolicyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}