using FinSync.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinSync.Persistence.Context;

public class FinSyncDbContext : DbContext
{
    public FinSyncDbContext(DbContextOptions<FinSyncDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}