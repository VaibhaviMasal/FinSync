using FinSync.Domain.Entities;
using FinSync.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Context;

public class FinSyncDbContext : DbContext
{
    public FinSyncDbContext(DbContextOptions<FinSyncDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public DbSet<InsuranceCompany> InsuranceCompanies { get; set; }

    public DbSet<InsurancePlan> InsurancePlans { get; set; }

    public DbSet<Policy> Policies { get; set; }

    public DbSet<PremiumPayment> PremiumPayments { get; set; }
    public DbSet<PolicyRenewal> PolicyRenewals { get; set; }

    public DbSet<Agent> Agents { get; set; }

    public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

    public DbSet<Document> Documents { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<Setting> Settings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinSyncDbContext).Assembly);

        modelBuilder.ApplyConfiguration(new AgentConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
    