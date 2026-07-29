using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.HasKey(a => a.AgentId);

            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.MiddleName)
                .HasMaxLength(50);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.MobileNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.AlternateMobileNumber)
                .HasMaxLength(10);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.PanNumber)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(a => a.AadhaarNumber)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(a => a.LicenseNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Pincode)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(a => a.IsActive)
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedDate)
                .IsRequired();

            builder.HasMany(a => a.Policies)
                .WithOne(p => p.Agent)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}