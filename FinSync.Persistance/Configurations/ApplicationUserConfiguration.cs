using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinSync.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // Primary Key
            builder.HasKey(x => x.UserId);

            builder.Property(x => x.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Email)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.HasIndex(x => x.Email)
                   .IsUnique();

            builder.Property(x => x.PasswordHash)
                   .IsRequired();

            builder.Property(x => x.Role)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.Property(x => x.IsActive)
                   .HasDefaultValue(true);

            builder.Property(x => x.CreatedDate)
        .IsRequired();
        }
    }
}