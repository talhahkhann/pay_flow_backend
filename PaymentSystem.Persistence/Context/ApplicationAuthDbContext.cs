using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaymentSystem.Domain.Auth;
using PaymentSystem.Persistence.Identity;
using PaymentSystem.Persistence.Indentity;

namespace PaymentSystem.Persistence.Auth
{
     /// <summary>
    /// Ensures all DateTime values are stored as UTC in PostgreSQL.
    /// Required because Npgsql rejects Kind=Local for 'timestamp with time zone'.
    /// </summary>
    public class AuthDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<OtpCode> OTPs => Set<OtpCode>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("auth");

            //  Apply UTC value converter to ALL DateTime and DateTime? properties
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(new UtcValueConverter());
                    }
                }
            }

            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}