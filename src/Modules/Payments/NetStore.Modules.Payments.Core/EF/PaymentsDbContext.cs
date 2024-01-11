using Microsoft.EntityFrameworkCore;
using NetStore.Modules.Payments.Core.Domain;

namespace NetStore.Modules.Payments.Core.EF;

internal sealed class PaymentsDbContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
    
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("payments");
    }
}