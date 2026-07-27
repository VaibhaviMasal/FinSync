using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class PremiumPaymentConfiguration : IEntityTypeConfiguration<PremiumPayment>
    {
        public void Configure(EntityTypeBuilder<PremiumPayment> builder)
        {
            // Primary Key
            builder.HasKey(x => x.PremiumPaymentId);

            // Amount
            builder.Property(x => x.Amount)
                   .HasPrecision(18, 2)
                   .IsRequired();

            // Dates
            builder.Property(x => x.DueDate)
                   .IsRequired();

            builder.Property(x => x.PaymentDate);

            // Enums
            builder.Property(x => x.PaymentMode)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(x => x.PaymentStatus)
                   .HasConversion<string>()
                   .IsRequired();

            // Receipt Number
            builder.Property(x => x.ReceiptNumber)
                   .HasMaxLength(50);

            // Transaction Reference
            builder.Property(x => x.TransactionReference)
                   .HasMaxLength(100);

            // Remarks
            builder.Property(x => x.Remarks)
                   .HasMaxLength(500);

            // Audit Fields
            builder.Property(x => x.CreatedDate)
                   .IsRequired();

            builder.Property(x => x.UpdatedDate);

            // Relationship
            builder.HasOne(x => x.Policy)
                   .WithMany(p => p.PremiumPayments)
                   .HasForeignKey(x => x.PolicyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}