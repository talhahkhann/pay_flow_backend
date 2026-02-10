using Microsoft.EntityFrameworkCore;

namespace PaymentSystem.Persistence.Payments
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("payments");
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(PaymentDbContext).Assembly);
        }
    }
}
